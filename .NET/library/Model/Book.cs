namespace OneBeyondApi.Model
{
    public class Book
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public Author? Author { get; set; }
        public Guid AuthorId { get; set; }

        public BookFormat Format { get; set; }

        public required string ISBN { get; set; }

        public BookStock? BookStock { get; set; }
    }
}
