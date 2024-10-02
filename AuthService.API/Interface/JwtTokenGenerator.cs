

using AuthService.API.Data;

namespace AuthService.API.Extensions
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        
    }
}