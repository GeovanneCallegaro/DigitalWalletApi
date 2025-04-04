using DigitalWallet.Application.DTOs.Wallet;

using FluentValidation;

namespace DigitalWallet.Application.Validators.Wallet
{
    public class TransferBalanceValidator : AbstractValidator<TransferBalanceDto>
    {
        public TransferBalanceValidator()
        {
            RuleFor(x => x.ReceiverUserId)
                .NotEmpty().WithMessage("O destinatário é obrigatório.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("O valor deve ser maior que 0.");
        }
    }
}
