using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.ApplicationCore.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Api.Features.Books.Queries
{
    public class GetBookListQuery : IRequest<List<BookDto>>
    {
        public class GetListQueryHandler : IRequestHandler<GetBookListQuery, List<BookDto>>
        {
            private readonly EfContext _context;
            private readonly IMapper _mapper;

            public GetListQueryHandler(EfContext contexto, IMapper mapper)
            {
                _context = contexto;
                _mapper = mapper;
            }

            public async Task<List<BookDto>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
            {
                List<Book> list = await _context.Book.ToListAsync();
                List<BookDto> listDto = _mapper.Map<List<Book>, List<BookDto>>(list);
                return listDto;
            }
        }
    }
}
