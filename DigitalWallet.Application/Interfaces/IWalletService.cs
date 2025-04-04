using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.Wallet;

namespace DigitalWallet.Application.Interfaces
{
    public interface IWalletService
    {
        Task<ResultData<GetBalanceByUserResponse>> GetBalanceByUserId(GetBalanceByUserDto getBalanceByUserDto);
        Task<ResultData<AddBalanceToUserResponse>> AddBalanceToUser(Guid userId, AddBalanceToUserDto addBalanceToUserDto);
        Task<ResultData<TransferBalanceResponse>> TransferBalance(Guid userId, TransferBalanceDto transferBalanceDto);
    }
}
