namespace OneBeyondApi.Model
{
    public class Borrower
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string EmailAddress { get; set; }

        public ICollection<BookStock>? BookStocks { get; set; }
        
        public ICollection<Fine>? Fines { get; set; }

        public ICollection<BookReservation>? BookReservations { get; set; }
    }
}
