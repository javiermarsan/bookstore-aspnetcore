using BookStore.Domain.Entities;
using BookStore.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Baskets.Commands
{
    public class CreateBasketCommand : IRequest<Guid>
    {
        public DateTime CreationDate { get; set; }

        public List<Guid> Items { get; set; }

        public class CreateCommandHandler : IRequestHandler<CreateBasketCommand, Guid>
        {
            private readonly IRepository<Basket> _repository;
            private readonly IBasketService _basketService;

            public CreateCommandHandler(IRepository<Basket> repository, IBasketService basketService)
            {
                _repository = repository;
                _basketService = basketService;
            }

            public async Task<Guid> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
            {
                Basket entity = new Basket
                {
                    CreationDate = request.CreationDate
                };

                _repository.Add(entity);

                int value = await _repository.SaveAsync();
                if (value == 0)
                    throw new Exception("Failed to save the basket");

                Guid basketId = entity.BasketId;
                if (request.Items == null || request.Items.Count == 0)
                    return basketId;

                value = await _basketService.AddItemsToBasket(basketId, request.Items);
                if (value > 0)
                    return basketId;

                throw new Exception("Failed to save the basket item");
            }
        }
    }
}
