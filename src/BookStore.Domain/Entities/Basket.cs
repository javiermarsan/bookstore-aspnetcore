using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Common;

namespace BookStore.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public Guid BasketId { get; set; }

        public DateTime CreationDate { get; set; }

        // DDD Patterns comment
        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so Items cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method Basket.AddItem() which includes behavior.
        private readonly List<BasketItem> _items = new List<BasketItem>();

        // Using List<>.AsReadOnly() 
        // This will create a read only wrapper around the private list so is protected against "external updates".
        // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
        //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

        public void AddItem(BasketItem item)
        {
            if (item != null)
                _items.Add(item);
        }
    }
}
