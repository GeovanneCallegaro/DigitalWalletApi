// <copyright file="IJwtService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string email, string userId);
    }
}
