// <copyright file="IAuthService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Interfaces
{
    using DigitalWallet.Application.Common;
    using DigitalWallet.Application.DTOs.Auth;

    public interface IAuthService
    {
        public Task<ResultData<LoginResponse>> Login(LoginDto login);
    }
}
