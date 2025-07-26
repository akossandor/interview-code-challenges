namespace OneBeyondApi.QueryResults
{
    public class CatalogueOnLoan
    {
        public CatalogueOnLoan(IReadOnlyList<BorrowerOnLoan> borrowersOnLoan, IReadOnlyList<string> bookNamesOnLoan)
        {
            BorrowersOnLoan = borrowersOnLoan ?? throw new ArgumentNullException(nameof(borrowersOnLoan));
            BookNamesOnLoan = bookNamesOnLoan;
        }

        public IReadOnlyList<BorrowerOnLoan> BorrowersOnLoan { get; }
        public IReadOnlyList<string>? BookNamesOnLoan { get; }
    }
}
