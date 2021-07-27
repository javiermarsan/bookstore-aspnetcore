using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Domain.Entities;

namespace BookStore.Application.Authors.Queries
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, AuthorDto>();
        }
    }
}
