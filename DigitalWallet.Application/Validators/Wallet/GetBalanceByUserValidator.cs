// <copyright file="GetBalanceByUserValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Validators.Wallet
{
    using DigitalWallet.Application.DTOs.Wallet;

    using FluentValidation;

    public class GetBalanceByUserValidator : AbstractValidator<GetBalanceByUserDto>
    {
        public GetBalanceByUserValidator()
        {
            this.RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("O UserId é obrigatório.");
        }
    }
}
