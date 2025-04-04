// <copyright file="IWalletService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Interfaces
{
    using DigitalWallet.Application.Common;
    using DigitalWallet.Application.DTOs.Wallet;

    public interface IWalletService
    {
        Task<ResultData<GetBalanceByUserResponse>> GetBalanceByUserId(GetBalanceByUserDto getBalanceByUserDto);

        Task<ResultData<AddBalanceToUserResponse>> AddBalanceToUser(Guid userId, AddBalanceToUserDto addBalanceToUserDto);

        Task<ResultData<TransferBalanceResponse>> TransferBalance(Guid userId, TransferBalanceDto transferBalanceDto);
    }
}
