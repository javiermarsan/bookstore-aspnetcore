using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data
{
    public class EfContext : DbContext
    {
        public EfContext() { }

        public EfContext(DbContextOptions<EfContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // The domain is independent of data access concerns
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfContext).Assembly);
        }
    }
}
