using DigitalWallet.Application.DTOs.User;
using DigitalWallet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DigitalWallet.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpPost]
        [SwaggerOperation(Summary = "Registrar um novo usuário", Description = "Cria um novo usuário no sistema.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Usuário criado com sucesso")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
        {
            var result = await _userService.CreateUser(request);

            if (result.IsSuccess)
                return StatusCode((int)result.StatusCode);

            return StatusCode((int)result.StatusCode, new { errors = result.Errors });
        }
    }
}
