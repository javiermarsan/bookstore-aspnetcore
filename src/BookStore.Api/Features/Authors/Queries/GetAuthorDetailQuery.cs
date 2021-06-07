using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.ApplicationCore.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Features.Authors.Queries
{
    public class GetAuthorDetailQuery : IRequest<AuthorDto>
    {
        public Guid? AuthorId { get; set; }

        public class GetDetailQueryHandler : IRequestHandler<GetAuthorDetailQuery, AuthorDto>
        {
            private readonly EfContext _context;
            private readonly IMapper _mapper;

            public GetDetailQueryHandler(EfContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AuthorDto> Handle(GetAuthorDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Author.Where(x => x.AuthorId == request.AuthorId).FirstOrDefaultAsync();
                if (entity == null)
                    throw new Exception("Author not found");

                var dto = _mapper.Map<AuthorEntity, AuthorDto>(entity);
                return dto;
            }
        }
    }
}
