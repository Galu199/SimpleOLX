using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SimpleOLX.Entities;

namespace SimpleOLX.Services
{
    /// <summary>
    /// TODO
    /// </summary>
    public class JWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="user"> user </param>
        /// <returns> string </returns>
        public string CreateJWT(User user)
        {
            return new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: new Claim[]
                        {
                            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                            new Claim(ClaimTypes.GivenName, user.FirstName),
                            new Claim(ClaimTypes.Surname, user.LastName),
                            new Claim(ClaimTypes.Email, user.Email!)
                        },
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:ExpirationTimeInMinutes"]!)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]!)), SecurityAlgorithms.HmacSha256)
                )
            );
        }
    }
}
