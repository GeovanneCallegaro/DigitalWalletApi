using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.Auth;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Repositories;
using DigitalWallet.Infrastructure.Security;
using FluentValidation;
using System.Net;

namespace DigitalWallet.Application.Services
{
    public class AuthService(IUnitOfWork unitOfWork, IJwtService jwtService, IValidator<LoginDto> validator) : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IValidator<LoginDto> _validator = validator;

        public async Task<Result<string>> Login(LoginDto loginDto)
        {
            var validationResult = await _validator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                return Result<string>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            }

            var user = await _unitOfWork.Users.GetByEmailAsync(loginDto.Email);
            if (user == null)
                return Result<string>.Failure($"Usuário não encontrado.", HttpStatusCode.NotFound);

            var verifyPassword = PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash);
            if (!verifyPassword)
                return Result<string>.Failure("Acesso negado. Senha incorreta.", HttpStatusCode.Unauthorized);

            var token = _jwtService.GenerateToken(user.Email, user.Id.ToString());
            return Result<string>.Success(token, HttpStatusCode.OK);
        }
    }
}
