using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.User;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Repositories;
using DigitalWallet.Infrastructure.Security;
using FluentValidation;
using System.Net;

namespace DigitalWallet.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork, IValidator<RegisterUserDto> validator) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IValidator<RegisterUserDto> _validator = validator;

        public async Task<Result> CreateUser(RegisterUserDto registerUserDto)
        {
            var validationResult = await _validator.ValidateAsync(registerUserDto);
            if (!validationResult.IsValid)
            {
                return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var existingUser = await _unitOfWork.Users.GetByEmailAsync(registerUserDto.Email);

            if (existingUser is not null)
            {
                return Result.Failure("E-mail já cadastrado.", HttpStatusCode.Conflict);
            }

            var hashedPassword = PasswordHasher.HashPassword(registerUserDto.Password);

            var user = new User(registerUserDto.Name, registerUserDto.Email, hashedPassword);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.Success(HttpStatusCode.Created);
        }
    }
}
