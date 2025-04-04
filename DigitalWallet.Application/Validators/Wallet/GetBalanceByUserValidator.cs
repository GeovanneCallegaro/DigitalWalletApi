using DigitalWallet.Application.DTOs.Wallet;

using FluentValidation;

namespace DigitalWallet.Application.Validators.Wallet
{
    public class GetBalanceByUserValidator : AbstractValidator<GetBalanceByUserDto>
    {
        public GetBalanceByUserValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("O UserId é obrigatório.");
        }
    }
}
