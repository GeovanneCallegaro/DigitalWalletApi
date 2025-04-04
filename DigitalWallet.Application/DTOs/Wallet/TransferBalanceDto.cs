// <copyright file="TransferBalanceDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.DTOs.Wallet
{
    public class TransferBalanceDto
    {
        public Guid ReceiverUserId { get; set; }

        public decimal Amount { get; set; }
    }
}
