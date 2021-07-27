using AutoMapper;
using MediatR;
using BookStore.Application.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Interfaces;

namespace BookStore.Api.Features.Authors.Queries
{
    public class GetAuthorListQuery : IRequest<List<AuthorDto>>
    {
        public class GetListQueryHandler : IRequestHandler<GetAuthorListQuery, List<AuthorDto>>
        {
            private readonly IRepository<Author> _repository;
            private readonly IMapper _mapper;

            public GetListQueryHandler(IRepository<Author> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<AuthorDto>> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
            {
                List<Author> list = await _repository.Query().ToListAsync();
                List<AuthorDto> listDto = _mapper.Map<List<Author>, List<AuthorDto>>(list);
                return listDto;
            }
        }
    }
}
