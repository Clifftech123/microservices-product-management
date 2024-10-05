

using AuthService.API.Data;

namespace AuthService.API.Extensions
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser user);
        
    }
}