using AuthService.API.Contracts;
using AuthService.API.Data;
using AuthService.API.Extensions;
using AuthService.API.Interface;
using Microsoft.AspNetCore.Identity;

namespace AuthService.API.Services
{
    /// <summary>
    /// Service for managing user operations.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="jwtTokenGenerator">The JWT token generator.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, ILogger<AuthenticationService> logger)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginUserRequest">The login user request.</param>
        /// <returns>The login response.</returns>
        public async Task<LoginResponse> LoginAsync(LoginUserRequest loginUserRequest)
        {
            var user = await _userManager.FindByNameAsync(loginUserRequest.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserRequest.Password))
            {
                _logger.LogWarning("Invalid login attempt for user {Email}", loginUserRequest.Email);
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid userName or password."
                };
            }

            var token = await _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                Success = true,
                Token = token,
                Email = user.Email,
                Id = user.Id,
                Message = "Login successful  successfully."
            };
        }


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerUserRequest">The register user request.</param>
        /// <returns>The login response.</returns>
        public async Task<LoginResponse> RegisterAsync(RegisterUserRequest registerUserRequest)
        {
            var existingUser = await _userManager.FindByNameAsync(registerUserRequest.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration attempt with existing email {Email}", registerUserRequest.Email);
                return new LoginResponse
                {
                    Success = false,
                    Message = "User already exists."
                };
            }
            // Validate the password

            if (!registerUserRequest.Password.Any(char.IsDigit))
            {

                _logger.LogWarning("Registration attempt with existing email {Email}", registerUserRequest.Email);
                return new LoginResponse
                {
                    Success = false,
                    Message = "Password must contain at least one digit."
                };
            }

            var newUser = new ApplicationUser
            {
                UserName = registerUserRequest.Email,
                Email = registerUserRequest.Email
            };

            var result = await _userManager.CreateAsync(newUser, registerUserRequest.Password);
            if (!result.Succeeded)
            {
                _logger.LogError("User registration failed for {Email}: {Errors}", registerUserRequest.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                return new LoginResponse
                {
                    Success = false,
                    Message = "User registration failed."
                };
            }

            var token = await _jwtTokenGenerator.GenerateToken(newUser);


            return new LoginResponse
            {
                Success = true,
                Token = token,

                Message = "Account created successfully."
            };
        }
    }
}
