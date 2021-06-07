using AutoMapper;
using MediatR;
using BookStore.ApplicationCore.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Api.Features.Authors.Queries
{
    public class GetAuthorListQuery : IRequest<List<AuthorDto>>
    {
        public class GetListQueryHandler : IRequestHandler<GetAuthorListQuery, List<AuthorDto>>
        {
            private readonly EfContext _context;
            private readonly IMapper _mapper;

            public GetListQueryHandler(EfContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AuthorDto>> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
            {
                List<AuthorEntity> list = await _context.Author.ToListAsync();
                List<AuthorDto> listDto = _mapper.Map<List<AuthorEntity>, List<AuthorDto>>(list);
                return listDto;
            }
        }
    }
}
