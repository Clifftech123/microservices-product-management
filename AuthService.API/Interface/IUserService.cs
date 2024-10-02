

using AuthService.API.Contracts;

namespace AuthService.API.Interface
{
    public interface IUserService
    {
     Task<LoginResponse> RegisterAsync(RegisterUserRequest registerUserRequest);

        Task<LoginResponse> LoginAsync(LoginUserRequest  loginUserRequest);
    }
}