using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data
{
    public class EfContext : DbContext
    {
        public EfContext() { }

        public EfContext(DbContextOptions<EfContext> options) : base(options) { }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Basket> Basket { get; set; }
        public virtual DbSet<BasketItem> BasketItem { get; set; }

    }
}
