using AuthService.API.Contracts;
using AuthService.API.Exceptions;
using AuthService.API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related operations.
    /// </summary>
    [ApiController]
    [Route("api/")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginUserRequest">The login user request.</param>
        /// <returns>The login response.</returns>
        [HttpPost("users/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]

        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest loginUserRequest)
        {
            var response = await _authenticationService.LoginAsync(loginUserRequest);
            if (response.Success)
            {
                return Ok(response);
            }

            throw new LoginException("Error occurred when trying to login.");
        }


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerUserRequest">The register user request.</param>
        /// <returns>The login response.</returns>
        [HttpPost("users/register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest registerUserRequest)
        {
            var response = await _authenticationService.RegisterAsync(registerUserRequest);
            if (response.Success)
            {
                return Created("", response);
            }

            throw new RegisterException("Error occurred when trying to register.");
        }
    }
}
