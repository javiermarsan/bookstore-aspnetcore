using BookStore.Application.Entities;
using BookStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Features.Baskets.Queries
{
    public class GetBasketDetailQuery : IRequest<BasketDto>
    {
        public Guid? BasketId { get; set; }

        public class GetDetailQueryHandler : IRequestHandler<GetBasketDetailQuery, BasketDto>
        {
            private readonly IEfContext _context;

            public GetDetailQueryHandler(IEfContext context)
            {
                _context = context;
            }

            public async Task<BasketDto> Handle(GetBasketDetailQuery request, CancellationToken cancellationToken)
            {
                List<BasketItemDto> listDto = new List<BasketItemDto>();
                Basket entity = await _context.Basket.FirstOrDefaultAsync(x => x.BasketId == request.BasketId);
                List<BasketItem> entityItems = await _context.BasketItem.Where(x => x.BasketId == request.BasketId).ToListAsync();

                foreach (BasketItem item in entityItems)
                {
                    Book book = await _context.Book.Where(x => x.BookId == item.ProductId).FirstOrDefaultAsync();
                    if (book != null)
                    {
                        BasketItemDto itemDto = new BasketItemDto
                        {
                            BasketItemId = item.Id,
                            BookId = book.BookId,
                            BookTitle = book.Title,
                            PublicationDate = book.PublicationDate,
                            AuthorId = book.AuthorId
                        };
                        listDto.Add(itemDto);
                    }
                }

                BasketDto basketDto = new BasketDto
                {
                    BasketId = entity.BasketId,
                    CreationDate = entity.CreationDate,
                    Items = listDto
                };

                return basketDto;
            }
        }
    }
}
