using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Common;

namespace BookStore.Domain.Entities
{
    public class Book : BaseEntity
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public Guid AuthorId { get; set; }

        public DateTime? PublicationDate { get; set; }
    }
}
