﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.ApplicationCore.Entities;
using BookStore.Infrastructure.Data;
using BookStore.ApplicationCore.Interfaces;

namespace BookStore.Api.Features.Books.Queries
{
    public class GetBookListQuery : IRequest<List<BookDto>>
    {
        public class GetListQueryHandler : IRequestHandler<GetBookListQuery, List<BookDto>>
        {
            private readonly IRepository<Book> _repository;
            private readonly IMapper _mapper;

            public GetListQueryHandler(IRepository<Book> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<BookDto>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
            {
                List<Book> list = await _repository.Query().ToListAsync();
                List<BookDto> listDto = _mapper.Map<List<Book>, List<BookDto>>(list);
                return listDto;
            }
        }
    }
}
