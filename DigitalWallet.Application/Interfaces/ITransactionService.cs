using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.Transaction;

namespace DigitalWallet.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<ResultData<List<GetTransactionsByUserResponse>>> GetUserTransactions(GetTransactionsByUserDto transactionsByUserDto);
    }
}
