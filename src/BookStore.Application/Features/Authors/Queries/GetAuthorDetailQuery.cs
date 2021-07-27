using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Entities;
using Microsoft.EntityFrameworkCore;
using BookStore.Application.Interfaces;

namespace BookStore.Application.Features.Authors.Queries
{
    public class GetAuthorDetailQuery : IRequest<AuthorDto>
    {
        public Guid? AuthorId { get; set; }

        public class GetDetailQueryHandler : IRequestHandler<GetAuthorDetailQuery, AuthorDto>
        {
            private readonly IRepository<Author> _repository;
            private readonly IMapper _mapper;

            public GetDetailQueryHandler(IRepository<Author> repository, IMapper mapper)
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
