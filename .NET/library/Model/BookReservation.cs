namespace OneBeyondApi.Model
{
    public class BookReservation
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public Borrower? ReservedBy { get; set; }
        public Guid? ReservedById { get; set; }
        
        public required DateTime ReservationDateTime { get; set; }
    }
}
