using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.Wallet;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Repositories;
using FluentValidation;
using System.Net;

namespace DigitalWallet.Application.Services
{
    public class WalletService(IUnitOfWork unitOfWork, IValidator<GetBalanceByUserDto> validator) : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IValidator<GetBalanceByUserDto> _validator = validator;

        public async Task<Result<GetBalanceByUserResponse>> GetBalanceByUserId(GetBalanceByUserDto getBalanceByUserDto)
        {
            var validationResult = await _validator.ValidateAsync(getBalanceByUserDto);
            if (!validationResult.IsValid)
            {
                return Result<GetBalanceByUserResponse>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var user = await _unitOfWork.Users.GetByIdAsync(getBalanceByUserDto.UserId);

            if (user is null || user.Wallet is null)
                return Result<GetBalanceByUserResponse>.Failure("Não foi encontrado um usuário com o Id informado", HttpStatusCode.NotFound);

            return Result<GetBalanceByUserResponse>.Success(new GetBalanceByUserResponse() { Balance = user.Wallet.Balance});
        }
    }
}
