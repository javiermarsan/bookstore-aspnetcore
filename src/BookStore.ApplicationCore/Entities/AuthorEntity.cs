using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ApplicationCore.Entities
{
    [Table("Author")]
    public class AuthorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}
