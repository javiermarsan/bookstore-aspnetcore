namespace BookStore.Application.Features.Token.Models
{
    public class Token
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int Expiry { get; set; }

        public int RefreshExpiry { get; set; }
    }
}
