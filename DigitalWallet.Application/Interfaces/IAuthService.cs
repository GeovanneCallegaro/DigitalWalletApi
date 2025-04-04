using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.Auth;

namespace DigitalWallet.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<ResultData<LoginResponse>> Login(LoginDto login);
    }
}
