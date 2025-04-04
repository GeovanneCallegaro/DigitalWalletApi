// <copyright file="TransferBalanceValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Validators.Wallet
{
    using DigitalWallet.Application.DTOs.Wallet;

    using FluentValidation;

    public class TransferBalanceValidator : AbstractValidator<TransferBalanceDto>
    {
        public TransferBalanceValidator()
        {
            this.RuleFor(x => x.ReceiverUserId)
                .NotEmpty().WithMessage("O destinatário é obrigatório.");

            this.RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("O valor deve ser maior que 0.");
        }
    }
}
