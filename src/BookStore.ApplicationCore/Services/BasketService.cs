using BookStore.ApplicationCore.Entities;
using BookStore.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<BasketItemEntity> _basketItemRepository;

        public BasketService(IRepository<BasketItemEntity> basketItemRepository)
        {
            _basketItemRepository = basketItemRepository;
        }

        public async Task<int> AddItemsToBasket(Guid basketId, List<Guid> itemsId)
        {
            foreach (Guid productId in itemsId)
            {
                BasketItemEntity item = new BasketItemEntity
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
