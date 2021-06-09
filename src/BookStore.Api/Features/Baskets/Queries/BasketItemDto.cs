using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Features.Baskets.Queries
{
    public class BasketItemDto
    {
        public Guid BasketItemId { get; set; }

        public Guid BookId { get; set; }

        public string BookTitle { get; set; }

        public DateTime? PublicationDate { get; set; }

        public Guid AuthorId { get; set; }
    }
}
