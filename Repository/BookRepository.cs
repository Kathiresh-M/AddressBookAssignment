using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookRepository : DbContext
    {
        public BookRepository(DbContextOptions<BookRepository> options) :
           base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<AddressBook> AddressBooks { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<RefSet> RefSets { get; set; }

        public DbSet<RefTerm> RefTerms { get; set; }

        public DbSet<RefSetTerm> RefSetTerm { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasAlternateKey(user => user.UserName);

            base.OnModelCreating(modelBuilder);
        }
    }
}
