using BookStore.Domain.Entities;
using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Baskets.Queries
{
    public class GetBasketDetailQuery : IRequest<BasketDto>
    {
        public Guid? BasketId { get; set; }

        public class GetDetailQueryHandler : IRequestHandler<GetBasketDetailQuery, BasketDto>
        {
            private readonly IBasketRepository _basketRepository;
            private readonly IRepository<Book> _bookRepository;

            public GetDetailQueryHandler(IBasketRepository basketRepository, IRepository<Book> bookRepository)
            {
                _basketRepository = basketRepository;
                _bookRepository = bookRepository;
            }

            public async Task<BasketDto> Handle(GetBasketDetailQuery request, CancellationToken cancellationToken)
            {
                List<BasketItemDto> listDto = new List<BasketItemDto>();
                Basket entity = await _basketRepository.GetByIdWithItemsAsync(request.BasketId.Value);

                foreach (BasketItem item in entity.Items)
                {
                    Book book = await _bookRepository.Query().Where(x => x.BookId == item.ProductId).FirstOrDefaultAsync();
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
