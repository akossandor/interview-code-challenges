namespace OneBeyondApi.QueryResults
{
    public class BorrowerOnLoan
    {
        public required string BorrowerName { get; set; }
        public required string BorrowerEmailAddress { get; set; }
        
        public required IReadOnlyList<string> BookNames { get; set; }
    }
}
