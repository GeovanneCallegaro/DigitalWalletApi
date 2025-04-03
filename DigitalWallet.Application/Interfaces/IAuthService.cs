using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.Auth;

namespace DigitalWallet.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<Result<string>> Login(LoginDto login);
    }
}
