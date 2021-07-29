using BookStore.Domain.Entities;
using BookStore.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Baskets.Services
{
    public class BasketService : IBasketService
    {
        private readonly ICatalogRepository<BasketItem> _basketItemRepository;

        public BasketService(ICatalogRepository<BasketItem> basketItemRepository)
        {
            _basketItemRepository = basketItemRepository;
        }

        public async Task<int> AddItemsToBasket(Guid basketId, List<Guid> itemsId)
        {
            foreach (Guid productId in itemsId)
            {
                BasketItem item = new BasketItem
                {
                    CreationDate = DateTime.Now,
                    BasketId = basketId,
                    ProductId = productId
                };

                _basketItemRepository.Add(item);
            }

            return await _basketItemRepository.SaveAsync();
        }
    }
}
