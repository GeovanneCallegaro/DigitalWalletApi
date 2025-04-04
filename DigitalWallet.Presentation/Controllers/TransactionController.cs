using DigitalWallet.Application.DTOs.Transaction;
using DigitalWallet.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DigitalWallet.Presentation.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    [Authorize]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService) => _transactionService = transactionService;

        [HttpGet]
        [SwaggerOperation(Summary = "Listar transações do usuário", Description = "Lista todas as transferências feitas pelo usuário, com filtro opcional por período.")]
        [ProducesResponseType(typeof(List<GetTransactionsByUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactions([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
           var authenticatedUserId = User.FindFirst("userId")?.Value;

            var dto = new GetTransactionsByUserDto() 
            {
                UserId = Guid.Parse(authenticatedUserId),
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await _transactionService.GetUserTransactions(dto);

            if (result.IsSuccess)
                return StatusCode((int)result.HttpStatusCode, result);

            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
