using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Common;

namespace BookStore.Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid ProductId { get; set; }

        public Guid BasketId { get; set; }
    }
}
