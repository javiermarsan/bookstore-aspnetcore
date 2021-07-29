using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IBasketRepository : ICatalogRepository<Basket>
    {
        Task<Basket> GetByIdWithItemsAsync(Guid id);
    }
}
