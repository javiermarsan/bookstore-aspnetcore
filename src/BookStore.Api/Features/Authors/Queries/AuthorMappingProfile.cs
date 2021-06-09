﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.ApplicationCore.Entities;

namespace BookStore.Api.Features.Authors.Queries
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<AuthorEntity, AuthorDto>();
        }
    }
}