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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid BasketId { get; set; }
    }
}
