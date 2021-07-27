using AutoMapper;
using BookStore.Domain.Entities;
using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries
{
    public class GetBookDetailQuery : IRequest<BookDto>
    {
        public Guid? BookId { get; set; }

        public class GetDetailQueryHandler : IRequestHandler<GetBookDetailQuery, BookDto>
        {
            private readonly IRepository<Book> _repository;
            private readonly IMapper _mapper;

            public GetDetailQueryHandler(IRepository<Book> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<BookDto> Handle(GetBookDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await _repository.Query().Where(x => x.BookId == request.BookId).FirstOrDefaultAsync();
                if (entity == null)
                    throw new Exception("Book not found");

                var dto = _mapper.Map<Book, BookDto>(entity);
                return dto;
            }
        }
    }
}
