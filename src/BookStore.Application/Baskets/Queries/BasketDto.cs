using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Baskets.Queries
{
    public class BasketDto
    {
        public Guid BasketId { get; set; }

        public DateTime CreationDate { get; set; }

        public List<BasketItemDto> Items { get; set; }
    }
}
