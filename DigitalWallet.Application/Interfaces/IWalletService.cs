using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.Wallet;

namespace DigitalWallet.Application.Interfaces
{
    public interface IWalletService
    {
        Task<Result<GetBalanceByUserResponse>> GetBalanceByUserId(GetBalanceByUserDto getBalanceByUserDto);
    }
}
