using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Entities
{
    public class Book : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BookId { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        public DateTime? PublicationDate { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}
