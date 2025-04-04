// <copyright file="AuthService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Services
{
    using System.Net;

    using DigitalWallet.Application.Common;
    using DigitalWallet.Application.DTOs.Auth;
    using DigitalWallet.Application.Interfaces;
    using DigitalWallet.Domain.Repositories;
    using DigitalWallet.Infrastructure.Security;

    using FluentValidation;

    public class AuthService(IUnitOfWork unitOfWork, IJwtService jwtService, IValidator<LoginDto> validator): IAuthService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IJwtService jwtService = jwtService;
        private readonly IValidator<LoginDto> validator = validator;

        public async Task<ResultData<LoginResponse>> Login(LoginDto loginDto)
        {
            var validationResult = await this.validator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                return ResultData<LoginResponse>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            }

            var user = await this.unitOfWork.Users.GetByEmailAsync(loginDto.email);
            if (user == null)
            {
                return ResultData<LoginResponse>.Error(["Usuário não encontrado."], HttpStatusCode.NotFound);
            }

            var verifyPassword = PasswordHasher.VerifyPassword(loginDto.password, user.PasswordHash);
            if (!verifyPassword)
            {
                return ResultData<LoginResponse>.Error(["Acesso negado. Senha incorreta."], HttpStatusCode.Unauthorized);
            }

            var token = this.jwtService.GenerateToken(user.Email, user.Id.ToString());
            return ResultData<LoginResponse>.Success(new LoginResponse() { Token = token }, HttpStatusCode.OK);
        }
    }
}
