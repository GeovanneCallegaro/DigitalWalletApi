using DigitalWallet.Application.Common;
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
        [ProducesResponseType(typeof(ResultData<GetBalanceByUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<GetBalanceByUserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultData<GetBalanceByUserResponse>), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetBalance([FromRoute] Guid userId)
        {
            var result = await _walletService.GetBalanceByUserId(new GetBalanceByUserDto() { UserId = userId });

            if (result.IsSuccess)
                return StatusCode((int)result.HttpStatusCode, result);

            return StatusCode((int)result.HttpStatusCode, result);

        }

        [HttpPost("/add-balance/{userId}")]
        [SwaggerOperation(Summary = "Adiciona saldo a carteira de um usuário", Description = "Adicione saldo a carteira de um determinado usuário.")]
        [ProducesResponseType(typeof(ResultData<AddBalanceToUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<AddBalanceToUserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultData<AddBalanceToUserResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultData<AddBalanceToUserResponse>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddBalance([FromRoute] Guid userId, [FromBody] AddBalanceToUserDto addBalanceToUserDto)
        {
            var result = await _walletService.AddBalanceToUser(userId, addBalanceToUserDto);

            if (result.IsSuccess)
                return StatusCode((int)result.HttpStatusCode, result);

            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpPost("/transfer")]
        [SwaggerOperation(Summary = "Realiza transferências entre carteiras", Description = "Realize transferência de sua carteira para um determinado usuário.")]
        [ProducesResponseType(typeof(ResultData<TransferBalanceResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultData<TransferBalanceResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultData<TransferBalanceResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TransferAmount([FromBody] TransferBalanceDto transferBalanceDto)
        {
            var authenticatedUserId = User.FindFirst("userId")?.Value;

            var result = await _walletService.TransferBalance(Guid.Parse(authenticatedUserId), transferBalanceDto);

            if (result.IsSuccess)
                return StatusCode((int)result.HttpStatusCode, result);

            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
