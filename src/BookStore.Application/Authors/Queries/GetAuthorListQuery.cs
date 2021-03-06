using AutoMapper;
using MediatR;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Common.Interfaces;

namespace BookStore.Application.Authors.Queries
{
    public class GetAuthorListQuery : IRequest<List<AuthorDto>>
    {
        public class GetListQueryHandler : IRequestHandler<GetAuthorListQuery, List<AuthorDto>>
        {
            private readonly ICatalogRepository<Author> _repository;
            private readonly IMapper _mapper;

            public GetListQueryHandler(ICatalogRepository<Author> repository, IMapper mapper)
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
