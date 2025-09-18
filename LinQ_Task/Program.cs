using LibraryManagementSystem;
using LINQ_DATA;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1.Find All Available Books
            var availableBooks = LibraryData.Books
                                .Where(book => book.IsAvailable == true);
            availableBooks.ToConsoleTable();
            //2. Get All Book Titles
            var allbooks = LibraryData.Books
                .Select(Book => Book);
            allbooks.ToConsoleTable();
            //3. Find Books by Genre
            var programmingbooks = LibraryData.Books.Where(book => book.Genre == "Programming");
            programmingbooks.ToConsoleTable();
            //4. Sort Books by Title
            var sortebbooks = LibraryData.Books.OrderBy(book => book.Title);
            sortebbooks.ToConsoleTable();
            //5. Find Expensive Books >30
            var Expensivebooks = LibraryData.Books.Where(Book => Book.Price > 30);
            Expensivebooks.ToConsoleTable();
            //6. Get Unique Genres
            var UniqueGenres = LibraryData.Books.Select(book => book.Genre).Distinct();
            UniqueGenres.ToConsoleTable();

            //7.Count Books by Genre
            var BooksbyGenre = LibraryData.Books
                                .GroupBy(book => book.Genre)
                                .Select(b => new
                                {
                                    BookGenre = b.Key,
                                    BookCount = b.Count()
                                });
            BooksbyGenre.ToConsoleTable();
            //#8. Find Recent Books
            var RecentBooks = LibraryData.Books.Where(book => book.PublishedYear > 2010);
            RecentBooks.ToConsoleTable();
            //9.Get First 5 Books
            var First5Books = LibraryData.Books.Where(book => book.Id <= 5).OrderBy(b => b.Id);
            First5Books.ToConsoleTable();
            //10.Check if Any Expensive Books Exist
            var ExpensiveBooksExist = LibraryData.Books.Where(book => book.Price > 50).Select(B => new
            {
                BOOK = B.Title,
                Isexpensive = B.IsAvailable
            });
            ExpensiveBooksExist.ToConsoleTable();
            //11.Books with Author Information
            var booksWithAuthors = LibraryData.Books
                                    .Join(LibraryData.Authors,
                                    book => book.AuthorId,
                                    author => author.Id,
                                    (book, author) => new
                                    {
                                        book.Title,
                                        AuthorName = author.Name,
                                        book.Genre
                                    });
            booksWithAuthors.ToConsoleTable();
            //12.Average Price by Genre
            var AveragePricebyGenre = LibraryData.Books.GroupBy(book => book.Genre).Select(B => new
            {
                Genre = B.Key,
                AveragePrice = B.Average(p => p.Price)
            });
            AveragePricebyGenre.ToConsoleTable();
            //13.Most Expensive Book
            var MostExpensiveBook = LibraryData.Books.OrderByDescending(book => book.Price).First();
            Console.WriteLine(MostExpensiveBook.Title);

            //14.Group Books by Published Decade
            var booksByPublishedDecade = LibraryData.Books
                .GroupBy(b => (b.PublishedYear / 10) * 10)
                .Select(g => new
                {
                    Decade = $"{g.Key}s",
                    Books = g.Select(b => b.Title).ToList()
                });

            booksByPublishedDecade.ToConsoleTable();
            // 15.Members with Active Loans
            var membersWithLoans = LibraryData.Members
                                   .GroupJoin(LibraryData.Loans,
                                member => member.Id,
                                  loan => loan.MemberId,
                                           (member, loans) => new
                                           {
                                               MemberName = member.FullName,
                                               MembershipType = member.MembershipType,
                                               ActiveLoans = loans.Where(l => l.ReturnDate == null).Count(),
                                           });
            membersWithLoans.ToConsoleTable();
            //16.Books Borrowed More Than Once
            var booksBorrowedMoreThanOnce = LibraryData.Loans
                .GroupBy(loan => loan.BookId)
                .Where(g => g.Count() > 1)
                .Join(LibraryData.Books,
                      g => g.Key,
                      book => book.Id,
                      (g, book) => new
                      {
                          BookTitle = book.Title,
                          LoanCount = g.Count()
                      });
            booksBorrowedMoreThanOnce.ToConsoleTable();
            //17.Overdue Books
            var overdueBooks = LibraryData.Loans
                .Where(loan => loan.DueDate < DateTime.Now && loan.ReturnDate == null)
                .Join(LibraryData.Books,
                      loan => loan.BookId,
                      book => book.Id,
                      (loan, book) => new
                      {
                          BookTitle = book.Title,
                          loan.LoanDate,
                          loan.DueDate,
                          DaysOverdue = (DateTime.Now - loan.DueDate).Days
                      });
            overdueBooks.ToConsoleTable();
            //18.Author Book Counts
            var authorsWithBooks = LibraryData.Authors
           .GroupJoin(LibraryData.Books,
               author => author.Id,
               book => book.AuthorId,
               (author, books) => new
               {
                   AuthorName = author.Name,
                   BookCount = books.Count()
               }).OrderByDescending(B => B.BookCount);
            authorsWithBooks.ToConsoleTable();
            //19.Price Range Analysis#
            var priceRangeAnalysis = LibraryData.Books
          .GroupBy(book =>
            book.Price < 20 ? "Cheap" :
                  book.Price <= 40 ? "Medium" : "Expensive")
          .Select(g => new
          {
              PriceRange = g.Key,
              BookCount = g.Count()
          });
            priceRangeAnalysis.ToConsoleTable();
            //20.Member Loan Statistics#
            var MemberLoanStatistics = LibraryData.Members
         .GroupJoin(LibraryData.Loans,
             member => member.Id,
             loan => loan.MemberId,
             (member, loans) => new
             {
                 MemberName = member.FullName,
                 TotalLoans = loans.Count(),
                 ActiveLoans = loans.Count(l => l.ReturnDate == null),
                 AverageDaysBorrowed = loans.Any()
                       ? loans.Average(l => ((l.ReturnDate ?? DateTime.Now) - l.LoanDate).Days)
                     : 0
             });
            MemberLoanStatistics.ToConsoleTable();
        }
    }
}
