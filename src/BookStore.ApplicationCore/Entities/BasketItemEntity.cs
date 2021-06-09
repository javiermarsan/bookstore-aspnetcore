using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ApplicationCore.Entities
{
    [Table("BasketItem")]
    public class BasketItemEntity : BaseEntity
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
