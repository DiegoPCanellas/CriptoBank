using Data.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Data.Common
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public JwtTokenGenerator()
        {
        }

        public string GenerateToken(string cpfcnpj)
        {
            var key = Encoding.ASCII.GetBytes("9e7bb56bde0c2fa2b0648644d0884eef");
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, cpfcnpj)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = "CriptoBank",
                Audience = "CriptoBank",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
