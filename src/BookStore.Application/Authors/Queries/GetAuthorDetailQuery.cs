using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BookStore.Application.Common.Interfaces;

namespace BookStore.Application.Authors.Queries
{
    public class GetAuthorDetailQuery : IRequest<AuthorDto>
    {
        public Guid? AuthorId { get; set; }

        public class GetDetailQueryHandler : IRequestHandler<GetAuthorDetailQuery, AuthorDto>
        {
            private readonly ICatalogRepository<Author> _repository;
            private readonly IMapper _mapper;

            public GetDetailQueryHandler(ICatalogRepository<Author> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<AuthorDto> Handle(GetAuthorDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await _repository.Query().Where(x => x.AuthorId == request.AuthorId).FirstOrDefaultAsync();
                if (entity == null)
                    throw new Exception("Author not found");

                var dto = _mapper.Map<Author, AuthorDto>(entity);
                return dto;
            }
        }
    }
}
