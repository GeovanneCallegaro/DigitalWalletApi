// <copyright file="GetTransactionsByUserValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Validators.Transaction
{
    using DigitalWallet.Application.DTOs.Transaction;

    using FluentValidation;

    public class GetTransactionsByUserValidator : AbstractValidator<GetTransactionsByUserDto>
    {
        public GetTransactionsByUserValidator()
        {
            this.RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("O usuário responsável pelas transações precisa ser enviado.");
        }
    }
}
