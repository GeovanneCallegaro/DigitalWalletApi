// <copyright file="IUserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Interfaces
{
    using DigitalWallet.Application.Common;
    using DigitalWallet.Application.DTOs.User;

    public interface IUserService
    {
        public Task<ResultData<object>> CreateUser(RegisterUserDto registerUserDto);
    }
}
