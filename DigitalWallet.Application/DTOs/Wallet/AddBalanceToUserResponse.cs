// <copyright file="AddBalanceToUserResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.DTOs.Wallet
{
    public class AddBalanceToUserResponse
    {
        public decimal NewBalance { get; set; }

        public Guid UserId { get; set; }
    }
}
