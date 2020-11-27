using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PassKeeperAuthorizationService.Configuration
{
    public class TokenParametres
    {
        public int LifeTimeMinutes { get; set; }
        public string Issuser { get; set; }
        public string Audience { get; set; }
        public string IssuerSigningKey { get; set; }

        public string TokenProvider { get; set; }
        public string LoginProvider { get; set; }
        public string TokenName { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey
        {
            get => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(IssuerSigningKey));
        }
    }
}