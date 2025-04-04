using DigitalWallet.Application.DTOs.Wallet;

using FluentValidation;

namespace DigitalWallet.Application.Validators.Wallet
{
    public class AddBalanceToUserValidator : AbstractValidator<AddBalanceToUserDto>
    {
        public AddBalanceToUserValidator()
        {
            RuleFor(x => x.BalanceToAdd)
                .NotEqual(0).WithMessage("O valor a ser adicionado não pode ser zero.");
        }
    }
}
