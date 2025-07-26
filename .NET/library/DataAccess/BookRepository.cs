using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Dto.V1.Books;
using OneBeyondApi.Helpers;
using OneBeyondApi.Model;
using System.Linq;
using System.Transactions;

namespace OneBeyondApi.DataAccess
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext context;

        private const string _bookReservationLock = "BookReservationLock";
        private static object _bookReservationLockObject = new();

        public BookRepository(LibraryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Book> GetBooksAll()
        {
            return [.. context.Books];
        }

        public Guid AddBook(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return book.Id;
        }

        public async Task<BookReservationResponseDto> ReserveAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new BadHttpRequestException("Title cannot be null or empty.");
            }

            var reservedBy = await context.Borrowers.AsNoTracking().FirstAsync();

            lock (_bookReservationLockObject)
            {
                var reservation = context.BookReservations.AsNoTracking().SingleOrDefault(x => x.ReservedById == reservedBy.Id && x.Title == title);
                if (reservation != null)
                {
                    return new() { Result = $"You have already reserved the book '{title}' on {reservation.ReservationDateTime:yyyy-MM-dd} so you cannot reserve again." };
                }

                var countBooksAvailable = context.Catalogues.AsNoTracking().Count(x => !x.OnLoanToId.HasValue && x.Book.Name == title);
                if (countBooksAvailable > 0)
                {
                    return new() { Result = $"There {TextHelper.ProvideIsAre(countBooksAvailable)} {countBooksAvailable} book{TextHelper.ProvidePlural(countBooksAvailable)} available so you can borrow the book '{title}' so it cannot be reserved." };
                }

                var bookReservation = new BookReservation { ReservedById = reservedBy.Id, Title = title, ReservationDateTime = DateTime.UtcNow };
                context.BookReservations.Add(bookReservation);
                context.SaveChanges();
            }

            return new() { Result = "The book has been reserved successfully." };
        }

        public async Task<BookStatusResponseDto> StatusAsync(string title)
        {
            var countBooksAvailable = await context.Catalogues.AsNoTracking().CountAsync(x => !x.OnLoanToId.HasValue && x.Book.Name == title);
            if (countBooksAvailable > 0)
            {
                return new() { ResultText = $"There {TextHelper.ProvideIsAre(countBooksAvailable)} {countBooksAvailable} book{TextHelper.ProvidePlural(countBooksAvailable)} available so you can borrow the book." };
            }

            var booksOnLoan = context.Catalogues.AsNoTracking().Where(x => x.OnLoanToId.HasValue && x.Book.Name == title).OrderBy(x => x.LoanEndDate).ToList();
            if (booksOnLoan == null || !booksOnLoan.Any())
            {
                return new() { ResultText = $"There is no book available so you cannot borrow the book." };
            }

            var bookToBeReturnedEarliest = booksOnLoan.Where(x => x.LoanEndDate.HasValue).First();
            return new() { ResultText = $"You can borrow the book from {bookToBeReturnedEarliest.LoanEndDate.Value:yyyy-MM-dd} if it is returned on time" };
        }

        public async Task ReserveRelationalDatabaseAndMoreServersAsync(string title)
        {
            var reservedBy = await context.Borrowers.AsNoTracking().FirstAsync();

            using var scope = new TransactionScope(TransactionScopeOption.Required);

            await context.Database.ExecuteSqlRawAsync($"exec sp_getapplock '{_bookReservationLock}', 'exclusive'");

            var isReserved = await context.BookReservations.AsNoTracking().AnyAsync(x => x.Title == title);
            if (isReserved)
            {
                throw new InvalidOperationException($"The book '{title}' is already reserved.");
            }

            var isOnloan = context.Catalogues.AsNoTracking().Any(x => x.OnLoanToId.HasValue && x.Book.Name == title);
            if (isOnloan)
            {
                throw new InvalidOperationException($"The book '{title}' is not on loan so it cannot be reserved.");
            }

            var bookReservation = new BookReservation { Title = title, ReservedById = reservedBy.Id, ReservationDateTime = DateTime.UtcNow };
            context.BookReservations.Add(bookReservation);
            await context.SaveChangesAsync();

            scope.Complete();
        }
    }
}
