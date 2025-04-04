using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.Auth;
using DigitalWallet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DigitalWallet.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Autenticar usuário", Description = "Autentica um usuário e retorna um token JWT.")]
        [ProducesResponseType(typeof(ResultData<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<LoginResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultData<LoginResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultData<LoginResponse>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {
            var result = await _authService.Login(loginRequest);

            if(result.IsSuccess)
                return StatusCode((int)result.HttpStatusCode, result);

            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
