using Microsoft.IdentityModel.Tokens;

namespace GeoFlat.Server.Models.Authorization
{
    public class AuthorizationOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifeTime { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Secret));
        }
    }
}
