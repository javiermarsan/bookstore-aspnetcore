using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data
{
    public class EfContext : DbContext
    {
        public EfContext() { }

        public EfContext(DbContextOptions<EfContext> options) : base(options) { }

        public virtual DbSet<AuthorEntity> Author { get; set; }
        public virtual DbSet<BookEntity> Book { get; set; }

    }
}
