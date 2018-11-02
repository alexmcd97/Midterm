using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;


namespace Midterm
{
    public class Program
    {
        static void Main(string[] args)
        {
            Author AdamFreeman = new Author()
            {
                AuthorID = 1,
                FirstName = "Adam",
                LastName = "Freeman",
            };

            Author HaishiBai = new Author()
            {
                AuthorID = 2,
                FirstName = "Haishi",
                LastName = "Bai",
            };

            List<Book> Books = new List<Book>()
            {
               

                new Book()
                {
                    Title = "Pro ASP.NET Core MVC 2 7th ed. Edition",
                    Publisher = "Apress",
                    PublishDate = "October 25, 2017",
                    Author = AdamFreeman,
                    PageCount = 1017,
                },

                new Book()
                {
                    Title = "Pro Angular 6 3rd Edition",
                    Publisher = "Apress",
                    PublishDate = "October 10, 2018",
                    Author = AdamFreeman,
                    PageCount = 776,
                },

                new Book()
                {
                    Title = "Programming Microsoft Azure Service Fabric (Developer Reference) 2nd Edition",
                    Publisher = "Microsoft Press",
                    PublishDate = "May 25, 2018",
                    Author = HaishiBai,
                    PageCount = 528,
                },
            };
            using(var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
                if (!db.Books.Any())
                {
                db.Books.AddRange(Books);

                Console.WriteLine("Database Seeded.");
                }
                db.SaveChanges();

                //Prints All Books in Database to the Console
                Console.WriteLine("All Books in Database");
                foreach(Book b in Books)
                {
                    Console.WriteLine(b);
                }
                //Prints All books published by Apress
                var aPressFiltered = Books.Where(b => b.Publisher == "Apress");
                Console.WriteLine("\nAll Books Published by Apress");
                foreach(Book b in aPressFiltered){
                    Console.WriteLine(b);
                }
                //Prints the book whose Author's First Name is the Shortest
                Console.WriteLine("\nAll Books written by Adam Freeman");
                var authorFnamefiltered = Books.OrderBy(b => b.Author.FirstName.Length).Take(2);
                foreach(Book b in authorFnamefiltered)
                {
                    Console.WriteLine(b);
                }

                //Find a Book written by Adam
                Console.WriteLine("\nFirst book by an author named \"Adam\"");
                var findAuthorAdam = Books.Where(b => b.Author.FirstName == "Adam").Take(1);
                foreach(Book b in findAuthorAdam)
                {
                    Console.WriteLine(b);
                }

                //Find a book with a page count over 1000
                Console.WriteLine("\nFirst book whose page count is greater than 1000");
                var over1000pg = Books.Where(b => b.PageCount > 1000).Take(1);
                foreach(Book b in over1000pg)
                {
                    Console.WriteLine(b);
                }

                //All Books Ordered by Author Last Name
                Console.WriteLine("\nAll Books orderedby Author's Last Name");
                var lastNameOrdered = Books.OrderBy(b => b.Author.LastName);
                foreach(Book b in lastNameOrdered)
                {
                    Console.WriteLine(b);
                }

                //All books Sorted by descending book title
                Console.WriteLine("\nAll Books sorted by Title Descending");
                var titleDescending = Books.OrderByDescending(b => b.Title);
                foreach(Book b in titleDescending)
                {
                    Console.WriteLine(b);
                }

                //All books grouped by publisher
                Console.WriteLine("\nAll Books grouped by Publisher");
                var groupedPublisher = Books.GroupBy(b => b.Publisher);
                foreach(IGrouping<string, Book> group in groupedPublisher)
                {
                    Console.WriteLine(group.Key + ":");
                    foreach(Book b in group)
                    {
                        Console.WriteLine(b);
                    }
                }

                //All Books grouped by publisher, sorted by Author Last Name
                Console.WriteLine("\nAll Books grouped by Publisher, and Sorted by Author Last Name");
                var groupedPublisherAndSorted = from b in Books
                                        orderby b.Author.LastName
                                        group b by b.Publisher;
                                        
                foreach(IGrouping<string, Book> group in groupedPublisherAndSorted)
                {
                    Console.WriteLine(group.Key + ":");
                    foreach(Book b in group)
                    {
                        Console.WriteLine(b);
                    }
                }
            }

        }
    }
}
