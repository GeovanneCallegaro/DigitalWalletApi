// <copyright file="GetTransactionsByUserResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.DTOs.Transaction
{
    public class GetTransactionsByUserResponse
    {
        public Guid TransactionId { get; set; }

        public Guid FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
