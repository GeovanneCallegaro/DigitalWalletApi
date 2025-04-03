using DigitalWallet.Application.DTOs.Wallet;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Presentation.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DigitalWallet.Presentation.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService) => _walletService = walletService;

        [HttpGet("/balance/{userId}")]
        [ServiceFilter(typeof(UserIdAuthorizationFilter))]
        [SwaggerOperation(Summary = "Obter o saldo na carteira de um usuário.", Description = "Obtém o saldo da carteira de um determinado usuário.")]
        [ProducesResponseType(typeof(GetBalanceByUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetBalance([FromRoute] Guid userId) 
        {
            var result = await _walletService.GetBalanceByUserId(new GetBalanceByUserDto() { UserId = userId});

            if (result.IsSuccess)
                return StatusCode((int)result.StatusCode, result.Data);

            return StatusCode((int)result.StatusCode, new { errors = result.Errors });

        }

    }
}
