using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BookStore.Application.Features.Token.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string Email{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEnabled { get; set; }

        [IgnoreDataMember]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
