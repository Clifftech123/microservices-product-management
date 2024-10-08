
namespace AuthService.API.Contracts
{
    public class LoginResponse
    {
       public string Id { get; set; } = "";
        
         public string Email { get; set; } = "";
         public string Token { get; set; } = "";
         public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
    }
}