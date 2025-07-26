using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Dto.V1.Books;
using OneBeyondApi.Infrastructure;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion(ApiVersions.V1)]
    [ApiExplorerSettings(GroupName = ApiVersions.MajorV1User)]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookRepository _bookRepository;

        public BooksController(ILogger<BooksController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;   
        }

        [HttpGet]
        [Route("all")]
        public IList<Book> GetAll()
        {
            return _bookRepository.GetBooksAll();
        }

        [HttpPost]
        [Route("AddBook")]
        public Guid Post(Book book)
        {
            return _bookRepository.AddBook(book);
        }

        [HttpPost]
        [Route("reserve")]
        public async Task<BookReservationResponseDto> ReserveAsync([FromBody] BookReservationRequestDto request)
        {
            return await _bookRepository.ReserveAsync(request.Title);
        }

        [HttpPost]
        [Route("status")]
        public async Task<BookStatusResponseDto> StatusAsync([FromBody] BookStatusRequestDto request)
        {
            return await _bookRepository.StatusAsync(request.Title);
        }
    }
}