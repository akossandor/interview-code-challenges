using OneBeyondApi.Dto.V1.Books;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBookRepository
    {
        public List<Book> GetBooksAll();

        public Guid AddBook(Book book);
        
        public Task<BookReservationResponseDto> ReserveAsync(string title);

        Task<BookStatusResponseDto> StatusAsync(string title);
    }
}
