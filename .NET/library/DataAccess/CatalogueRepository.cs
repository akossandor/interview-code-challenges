using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;
using OneBeyondApi.QueryResults;

namespace OneBeyondApi.DataAccess
{
    public class CatalogueRepository : ICatalogueRepository
    {
        private readonly LibraryContext context;

        public CatalogueRepository(LibraryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IReadOnlyList<BookStock> GetAll()
        {
            return context.Catalogues
                .Include(x => x.Book)
                .ThenInclude(x => x.Author)
                .Include(x => x.OnLoanTo)
                .ToList();
        }

        public CatalogueOnLoan GetOnLoan(bool shouldListBooks)
        {
            var borrowersOnLoan = context.Catalogues.AsNoTracking()
                .Where(x => x.OnLoanToId.HasValue)
                .GroupBy(x => x.OnLoanToId.Value)
                .Select(x => new BorrowerOnLoan { BorrowerName = x.First().OnLoanTo.Name, BorrowerEmailAddress = x.First().OnLoanTo.EmailAddress, BookNames = x.Select(y => y.Book.Name).OrderBy(y => y).ToList() })
                .ToList();

            //var cataloguesOnLoan = context.Catalogue.AsNoTracking()
            //    .Where(x => x.OnLoanTo != null)
            //    .Select(x => new { BorrowerId = x.OnLoanTo.Id, BorrowerName = x.OnLoanTo.Name, BorrowerEmailAddress = x.OnLoanTo.EmailAddress, BookName = x.Book.Name })
            //    .ToList();

            //var borrowersOnLoan = cataloguesOnLoan.GroupBy(x => x.BorrowerId)
            //    .Select(x => new BorrowerOnLoan
            //    {
            //        BorrowerName = x.First().BorrowerName,
            //        BorrowerEmailAddress = x.First().BorrowerEmailAddress,
            //        BookNames = x.Select(y => y.BookName).OrderBy(y => y).ToList()
            //    })
            //    .ToList();

            List<string>? booksOnLoan = null;

            if (shouldListBooks)
            {
                booksOnLoan = borrowersOnLoan.SelectMany(x => x.BookNames).Distinct().OrderBy(x => x).ToList();
            }

            return new(borrowersOnLoan, booksOnLoan);
        }

        public IReadOnlyList<BookStock> SearchCatalogue(CatalogueSearch search)
        {
            var list = context.Catalogues
                .Include(x => x.Book)
                .ThenInclude(x => x.Author)
                .Include(x => x.OnLoanTo)
                .AsQueryable();

            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.Author))
                {
                    list = list.Where(x => x.Book.Author.Name.Contains(search.Author));
                }
                if (!string.IsNullOrEmpty(search.BookName))
                {
                    list = list.Where(x => x.Book.Name.Contains(search.BookName));
                }
            }

            return list.ToList();
        }
    }
}
