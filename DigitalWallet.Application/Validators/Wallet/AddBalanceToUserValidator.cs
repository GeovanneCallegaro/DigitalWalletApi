// <copyright file="AddBalanceToUserValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Validators.Wallet
{
    using DigitalWallet.Application.DTOs.Wallet;

    using FluentValidation;

    public class AddBalanceToUserValidator : AbstractValidator<AddBalanceToUserDto>
    {
        public AddBalanceToUserValidator()
        {
            this.RuleFor(x => x.BalanceToAdd)
                .NotEqual(0).WithMessage("O valor a ser adicionado não pode ser zero.");
        }
    }
}
