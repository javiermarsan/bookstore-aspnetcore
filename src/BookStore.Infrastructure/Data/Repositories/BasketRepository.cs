using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data.Repositories
{
    public class BasketRepository : EfRepository<Basket>, IBasketRepository
    {
        public BasketRepository(EfContext dbContext) : base(dbContext)
        {
        }

        public Task<Basket> GetByIdWithItemsAsync(Guid id)
        {
            return DbContext.Basket
                .Include(o => o.Items)
                .FirstOrDefaultAsync(x => x.BasketId == id);
        }
    }
}
