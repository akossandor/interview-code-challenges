using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBorrowerRepository
    {
        public IReadOnlyList<Borrower> GetBorrowersAll();

        Guid AddBorrower(Borrower borrower);
    }
}
