using DigitalWallet.Application.DTOs.Transaction;

using FluentValidation;

namespace DigitalWallet.Application.Validators.Transaction
{
    public class GetTransactionsByUserValidator : AbstractValidator<GetTransactionsByUserDto>
    {
        public GetTransactionsByUserValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("O usuário responsável pelas transações precisa ser enviado.");
        }
    }
}
