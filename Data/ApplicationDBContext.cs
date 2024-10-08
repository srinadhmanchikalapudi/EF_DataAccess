using EF_Models.Models; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EF_DataAccess.Data
{
    internal class ApplicationDBContext : DbContext //We also need a connection string. So, we need to inherit from DbContext. The connection is established on a function or a event handler. So, we override onConfiguring below.
    {
        public DbSet<Book> Books { get; set; } //Dbset <Book> is a collection of db table Books or a model
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<BookAuthorMap> BookAuthorMap { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder options) //This needs to be inside the ApplicationDBContext class. We need to provide options object of DbContextOptionsBuilder class to provide a bunch of options to establishing connection object
        {
            options.UseSqlServer("Data Source = T-BONE; Database = TutorialDB; Integrated Security = True; Trust Server Certificate = True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(9, 4);
            modelBuilder.Entity<Book>().HasData(
                new Book { BookID = 1, Title = "He-Man", ISBN = "12345", Price = 11.5, Publisher_Id = 2},
                new Book { BookID = 2, Title = "Bat Man", ISBN = "334455", Price = 55.67, Publisher_Id = 2}
                );
            var BookList = new Book[]
                {

                    new Book { BookID = 3, Title = "OnePiece", ISBN = "776543", Price = 56.78, Publisher_Id = 3},
                    new Book { BookID = 4, Title = "Harry Potter", ISBN = "3456785", Price = 34.56, Publisher_Id = 1}
                };

            modelBuilder.Entity<Book>().HasData(BookList);

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Publisher_Id = 1, Name = "JkRowling", Location = "NewYork" },
                new Publisher { Publisher_Id = 2, Name = "StanLee", Location = "LosAngeles" },
                new Publisher { Publisher_Id = 3, Name = "Oda", Location = "Japan" }
                );

            modelBuilder.Entity<BookAuthorMap>().HasKey(
                ba=> new {ba.BookID, ba.Author_Id}
                
                );
            //modelBuilder.Entity<BookAuthorMap>

        }

    }
    
}
