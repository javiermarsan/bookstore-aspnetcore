using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Domain.Entities;

namespace BookStore.Application.Books.Queries
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
