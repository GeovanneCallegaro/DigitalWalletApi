// <copyright file="LoginValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Validators.Auth
{
    using DigitalWallet.Application.DTOs.Auth;

    using FluentValidation;

    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            this.RuleFor(x => x.email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");

            this.RuleFor(x => x.password)
                .NotEmpty().WithMessage("Senha é obrigatória.");
        }
    }
}
