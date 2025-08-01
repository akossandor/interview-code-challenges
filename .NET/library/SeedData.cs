﻿using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi
{
    public static class SeedData
    {
        public static void SetInitialData(LibraryContext context)
        {
            var ernestMonkjack = new Author { Name = "Ernest Monkjack" };
            var sarahKennedy = new Author { Name = "Sarah Kennedy" };
            var margaretJones = new Author { Name = "Margaret Jones" };

            var clayBook = new Book { Name = Constants.Books.Titles.TheImportanceOfClay, Format = BookFormat.Paperback, Author = ernestMonkjack, ISBN = "1305718181" };
            var agileBook = new Book { Name = Constants.Books.Titles.AgileProjectManagementAPrimer, Format = BookFormat.Hardback, Author = sarahKennedy, ISBN = "1293910102" };
            var rustBook = new Book { Name = Constants.Books.Titles.RustDevelopmentCookbook, Format = BookFormat.Paperback, Author = margaretJones, ISBN = "3134324111" };

            var daveSmith = new Borrower { Name = "Dave Smith", EmailAddress = "dave@smithy.com" };
            var lianaJames = new Borrower { Name = "Liana James", EmailAddress = "liana@gmail.com" };

            var bookOnLoanUntilToday = new BookStock { Book = clayBook, OnLoanTo = daveSmith, LoanEndDate = DateTime.Now.Date };
            var bookNotOnLoan = new BookStock { Book = clayBook, OnLoanTo = null, LoanEndDate = null };
            var bookOnLoanUntilNextWeek = new BookStock { Book = agileBook, OnLoanTo = lianaJames, LoanEndDate = DateTime.Now.Date.AddDays(7) };
            var rustBookStock = new BookStock { Book = rustBook, OnLoanTo = null, LoanEndDate = null };

            context.Authors.Add(ernestMonkjack);
            context.Authors.Add(sarahKennedy);
            context.Authors.Add(margaretJones);

            context.Books.Add(clayBook);
            context.Books.Add(agileBook);
            context.Books.Add(rustBook);

            context.Borrowers.Add(daveSmith);
            context.Borrowers.Add(lianaJames);

            context.Catalogues.Add(bookOnLoanUntilToday);
            context.Catalogues.Add(bookNotOnLoan);
            context.Catalogues.Add(bookOnLoanUntilNextWeek);
            context.Catalogues.Add(rustBookStock);

            context.SaveChanges();
        }
    }
}
