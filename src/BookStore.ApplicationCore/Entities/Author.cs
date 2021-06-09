using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ApplicationCore.Entities
{
    public class Author : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}
