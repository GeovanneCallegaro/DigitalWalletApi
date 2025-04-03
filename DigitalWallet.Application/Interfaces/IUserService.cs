using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.User;

namespace DigitalWallet.Application.Interfaces
{
    public interface IUserService
    {
        public Task<Result> CreateUser(RegisterUserDto registerUserDto);
    }
}
