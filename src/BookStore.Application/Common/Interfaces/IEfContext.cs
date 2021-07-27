using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IEfContext
    {
        DbSet<Author> Author { get; set; }
        DbSet<Book> Book { get; set; }
        DbSet<Basket> Basket { get; set; }
        DbSet<BasketItem> BasketItem { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
