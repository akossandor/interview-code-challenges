using OneBeyondApi.Model;
using OneBeyondApi.QueryResults;

namespace OneBeyondApi.DataAccess
{
    public interface ICatalogueRepository
    {
        public IReadOnlyList<BookStock> GetAll();
        public CatalogueOnLoan GetOnLoan(bool shouldListBooks);

        public IReadOnlyList<BookStock> SearchCatalogue(CatalogueSearch search);
    }
}
