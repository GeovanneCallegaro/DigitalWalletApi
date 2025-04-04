// <copyright file="ITransactionService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Interfaces
{
    using DigitalWallet.Application.Common;
    using DigitalWallet.Application.DTOs.Transaction;

    public interface ITransactionService
    {
        Task<ResultData<List<GetTransactionsByUserResponse>>> GetUserTransactions(GetTransactionsByUserDto transactionsByUserDto);
    }
}
