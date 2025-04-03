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
        [SwaggerResponse(StatusCodes.Status200OK, "Token gerado com sucesso", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário ou senha inválidos")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {
            var result = await _authService.Login(loginRequest);

            if(result.IsSuccess)
                return StatusCode((int)result.StatusCode, result.Data);

            return StatusCode((int)result.StatusCode, result.Errors);
        }
    }
}
