using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ApplicationCore.Entities
{
    [Table("Basket")]
    public class BasketEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BasketId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
