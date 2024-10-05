namespace AuthService.API.Exceptions
{

    public class RegisterException : Exception
    {
        public RegisterException() { }
        public RegisterException(string message) : base(message) { }
    }
}
