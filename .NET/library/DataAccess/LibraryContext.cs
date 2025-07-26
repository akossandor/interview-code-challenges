﻿using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookStock> Catalogues { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<BookReservation> BookReservations { get; set; }
    }
}
