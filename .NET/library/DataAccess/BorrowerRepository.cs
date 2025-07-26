using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly LibraryContext context;

        public BorrowerRepository(LibraryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IReadOnlyList<Borrower> GetBorrowersAll()
        {
            return [.. context.Borrowers];
        }

        public Guid AddBorrower(Borrower borrower)
        {
            context.Borrowers.Add(borrower);
            context.SaveChanges();
            return borrower.Id;
        }
    }
}
