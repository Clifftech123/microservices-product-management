using AuthService.API.Data;
using AuthService.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.API.Services
{
    /// <summary>
    /// Service for generating JWT tokens.
    /// </summary>
    public class JwtTokenGeneratorServices : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _secretKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenGeneratorServices"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        /// <exception cref="ArgumentNullException">Thrown when the JWT key is not configured.</exception>
        public JwtTokenGeneratorServices(IConfiguration configuration)
        {
            _configuration = configuration;
            var key = _configuration.GetSection("JwtSettings")["key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "JWT key is not configured.");
            }
            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to generate the token.</param>
        /// <returns>A JWT token as a string.</returns>
        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var signingCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);
            var claims = await GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        /// <summary>
        /// Gets the claims for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to get the claims.</param>
        /// <returns>A list of claims.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user is null or the user name is null.</exception>
        private static async Task<List<Claim>> GetClaimsAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? throw new ArgumentNullException(nameof(user.UserName), "UserName cannot be null.")),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            return await Task.FromResult(claims);
        }

        /// <summary>
        /// Generates the token options.
        /// </summary>
        /// <param name="signingCredentials">The signing credentials.</param>
        /// <param name="claims">The claims to include in the token.</param>
        /// <returns>A <see cref="JwtSecurityToken"/> instance.</returns>
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            return new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );
        }
    }
}
