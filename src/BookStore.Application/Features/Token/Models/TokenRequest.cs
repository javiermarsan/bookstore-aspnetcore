using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Features.Token.Models
{
    public class TokenRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
