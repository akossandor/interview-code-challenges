namespace OneBeyondApi.Model
{
    public class Fine
    {
        public Guid Id { get; set; }

        public Borrower? Borrower { get; set; }
        public Guid BorrowerId { get; set; }

        public BookStock? BookStock { get; set; }
        public Guid BookStockId { get; set; }

        public required decimal Price { get; set; }
        public required DateTime DateOnLoaned { get; set; }
        public required DateTime DateReturnedTo { get; set; }
    }
}
