using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.API.Data;
using AuthService.API.Extensions;
using AuthService.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.API.Services
{
    /// <summary>
    /// Service for generating JWT tokens.
    /// </summary>
    public class JwtTokenGeneratorServices : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenGeneratorServices"/> class.
        /// </summary>
        /// <param name="jwtOptions">The JWT options.</param>
        public JwtTokenGeneratorServices(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to generate the token.</param>
        /// <returns>The generated JWT token.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user is null.</exception>
        public string GenerateToken(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName ?? string.Empty),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            var jwtKey = _jwtOptions.Secret;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
