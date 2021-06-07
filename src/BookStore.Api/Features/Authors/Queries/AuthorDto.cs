﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Features.Authors.Queries
{
    public class AuthorDto
    {
        public Guid AuthorId { get; set; }

        public string Name { get; set; }
    }
}
