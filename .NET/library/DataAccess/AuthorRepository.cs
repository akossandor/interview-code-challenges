using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext context;

        public AuthorRepository(LibraryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Author> GetAuthors()
        {
            return [.. context.Authors];
        }

        public Guid AddAuthor(Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();
            return author.Id;
        }
    }
}
