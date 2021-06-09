using BookStore.ApplicationCore.Entities;
using BookStore.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Api.Features.Basket.Commands
{
    public class CreateBasketCommand : IRequest<Guid>
    {
        public DateTime CreationDate { get; set; }

        public List<Guid> Items { get; set; }

        public class CreateCommandHandler : IRequestHandler<CreateBasketCommand, Guid>
        {
            private readonly EfContext _context;

            public CreateCommandHandler(EfContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
            {
                BasketEntity entity = new BasketEntity
                {
                    CreationDate = request.CreationDate
                };

                _context.Basket.Add(entity);

                int value = await _context.SaveChangesAsync();
                if (value == 0)
                    throw new Exception("Failed to save the basket");

                Guid basketId = entity.BasketId;

                foreach (Guid productId in request.Items)
                {
                    BasketItemEntity item = new BasketItemEntity
                    {
                        CreationDate = DateTime.Now,
                        BasketId = basketId,
                        ProductId = productId
                    };

                    _context.BasketItem.Add(item);
                }

                value = await _context.SaveChangesAsync();
                if (value > 0)
                    return basketId;

                throw new Exception("Failed to save the basket item");
            }
        }
    }
}
