// <copyright file="UserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Services
{
    using System.Net;

    using DigitalWallet.Application.Common;
    using DigitalWallet.Application.DTOs.User;
    using DigitalWallet.Application.Interfaces;
    using DigitalWallet.Domain.Entities;
    using DigitalWallet.Domain.Repositories;
    using DigitalWallet.Infrastructure.Security;

    using FluentValidation;

    public class UserService(IUnitOfWork unitOfWork, IValidator<RegisterUserDto> validator): IUserService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IValidator<RegisterUserDto> validator = validator;

        public async Task<ResultData<object>> CreateUser(RegisterUserDto registerUserDto)
        {
            var validationResult = await this.validator.ValidateAsync(registerUserDto);
            if (!validationResult.IsValid)
            {
                return ResultData<object>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            }

            var existingUser = await this.unitOfWork.Users.GetByEmailAsync(registerUserDto.email);

            if (existingUser is not null)
            {
                return ResultData<object>.Error(["E-mail já cadastrado."], HttpStatusCode.Conflict);
            }

            var hashedPassword = PasswordHasher.HashPassword(registerUserDto.password);

            var user = new User(registerUserDto.name, registerUserDto.email, hashedPassword);
            await this.unitOfWork.Users.AddAsync(user);
            await this.unitOfWork.CommitAsync();

            return ResultData<object>.Success(default, HttpStatusCode.Created);
        }
    }
}
