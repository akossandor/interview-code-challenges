using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Infrastructure;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion(ApiVersions.V1)]
    [ApiExplorerSettings(GroupName = ApiVersions.MajorV1User)]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(ILogger<AuthorsController> logger, IAuthorRepository authorRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;   
        }

        [HttpGet]
        [Route("GetAuthors")]
        public IList<Author> Get()
        {
            return _authorRepository.GetAuthors();
        }

        [HttpPost]
        [Route("AddAuthor")]
        public Guid Post(Author author)
        {
            return _authorRepository.AddAuthor(author);
        }
    }
}