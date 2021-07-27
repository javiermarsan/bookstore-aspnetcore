using BookStore.Application.Entities;
using BookStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<BasketItem> _basketItemRepository;

        public BasketService(IRepository<BasketItem> basketItemRepository)
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
