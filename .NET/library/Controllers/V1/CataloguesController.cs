using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Infrastructure;
using OneBeyondApi.Model;
using OneBeyondApi.QueryResults;
using System.Collections;

namespace OneBeyondApi.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion(ApiVersions.V1)]
    [ApiExplorerSettings(GroupName = ApiVersions.MajorV1User)]
    public class CataloguesController : ControllerBase
    {
        private readonly ILogger<CataloguesController> _logger;
        private readonly ICatalogueRepository _catalogueRepository;

        public CataloguesController(ILogger<CataloguesController> logger, ICatalogueRepository catalogueRepository)
        {
            _logger = logger;
            _catalogueRepository = catalogueRepository;   
        }

        [HttpGet]
        [Route("all")]
        public IReadOnlyList<BookStock> GetAll()
        {
            return _catalogueRepository.GetAll();
        }

        [HttpGet]
        [Route("onloan")]
        public CatalogueOnLoan GetOnLoans(bool? shouldListBooks = false)
        {
            return _catalogueRepository.GetOnLoan(shouldListBooks ?? false);
        }

        [HttpPost]
        [Route("search")]
        public IReadOnlyList<BookStock> Search(CatalogueSearch search)
        {
            return _catalogueRepository.SearchCatalogue(search);
        }
    }
}