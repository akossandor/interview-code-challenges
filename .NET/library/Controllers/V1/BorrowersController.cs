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
    public class BorrowersController : ControllerBase
    {
        private readonly ILogger<BorrowersController> _logger;
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowersController(ILogger<BorrowersController> logger, IBorrowerRepository borrowerRepository)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;   
        }

        [HttpGet]
        [Route("all")]
        public IReadOnlyList<Borrower> GetAll()
        {
            return _borrowerRepository.GetBorrowersAll();
        }

        [HttpPost]
        [Route("AddBorrower")]
        public Guid Post(Borrower borrower)
        {
            return _borrowerRepository.AddBorrower(borrower);
        }
    }
}