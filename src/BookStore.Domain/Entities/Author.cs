using BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Author : BaseEntity
    {
        public Guid AuthorId { get; set; }

        public string Name { get; set; }
    }
}
