// <copyright file="WalletService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Services
{
    using System.Net;

    using DigitalWallet.Application.Common;
    using DigitalWallet.Application.DTOs.Wallet;
    using DigitalWallet.Application.Interfaces;
    using DigitalWallet.Domain.Entities;
    using DigitalWallet.Domain.Repositories;

    using FluentValidation;

    public class WalletService(IUnitOfWork unitOfWork, IValidator<GetBalanceByUserDto> validatorToGetBalance,
        IValidator<AddBalanceToUserDto> validatorToAddBalance, IValidator<TransferBalanceDto> validatorToTransferBalance): IWalletService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IValidator<GetBalanceByUserDto> validatorToGetBalance = validatorToGetBalance;
        private readonly IValidator<AddBalanceToUserDto> validatorToAddBalance = validatorToAddBalance;
        private readonly IValidator<TransferBalanceDto> validatorToTransferBalance = validatorToTransferBalance;

        public async Task<ResultData<GetBalanceByUserResponse>> GetBalanceByUserId(GetBalanceByUserDto getBalanceByUserDto)
        {
            var validationResult = await this.validatorToGetBalance.ValidateAsync(getBalanceByUserDto);
            if (!validationResult.IsValid)
            {
                return ResultData<GetBalanceByUserResponse>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            }

            var user = await this.unitOfWork.Users.GetByIdAsync(getBalanceByUserDto.UserId);

            if (user is null)
            {
                return ResultData<GetBalanceByUserResponse>.Error(["Não foi encontrado um usuário com o Id informado"], HttpStatusCode.NotFound);
            }

            var wallet = await this.unitOfWork.Wallets.GetByUser(user.Id);

            return ResultData<GetBalanceByUserResponse>.Success(new GetBalanceByUserResponse() { Balance = wallet.Balance }, HttpStatusCode.OK);
        }

        public async Task<ResultData<AddBalanceToUserResponse>> AddBalanceToUser(Guid userId, AddBalanceToUserDto addBalanceToUserDto)
        {
            var validationResult = await this.validatorToAddBalance.ValidateAsync(addBalanceToUserDto);
            if (!validationResult.IsValid)
            {
                return ResultData<AddBalanceToUserResponse>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            }

            var user = await this.unitOfWork.Users.GetByIdAsync(userId);

            if (user is null)
            {
                return ResultData<AddBalanceToUserResponse>.Error(["Não foi encontrado um usuário com o Id informado"], HttpStatusCode.NotFound);
            }

            var wallet = await this.unitOfWork.Wallets.GetByUser(userId);
            wallet.Deposit(addBalanceToUserDto.BalanceToAdd);

            this.unitOfWork.Wallets.Update(wallet);
            await this.unitOfWork.CommitAsync();

            return ResultData<AddBalanceToUserResponse>.Success(new AddBalanceToUserResponse() { NewBalance = wallet.Balance, UserId = userId }, HttpStatusCode.OK);
        }

        public async Task<ResultData<TransferBalanceResponse>> TransferBalance(Guid userId, TransferBalanceDto transferBalanceDto)
        {
            var validationResult = await this.validatorToTransferBalance.ValidateAsync(transferBalanceDto);
            if (!validationResult.IsValid)
            {
                return ResultData<TransferBalanceResponse>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            }

            var toUser = await this.unitOfWork.Users.GetByIdAsync(transferBalanceDto.ReceiverUserId);

            if (toUser is null)
            {
                return ResultData<TransferBalanceResponse>.Error(["Não foi encontrado o usuário destinatário dessa transação"], HttpStatusCode.NotFound);
            }

            var fromWallet = await this.unitOfWork.Wallets.GetByUser(userId);
            var toWallet = await this.unitOfWork.Wallets.GetByUser(toUser.Id);

            if (fromWallet.Balance < transferBalanceDto.Amount)
            {
                return ResultData<TransferBalanceResponse>.Error(["Saldo insuficiente."], HttpStatusCode.UnprocessableEntity);
            }

            fromWallet.TransferTo(toWallet, transferBalanceDto.Amount);

            var transaction = new Transaction(fromWallet.Id, toWallet.Id, transferBalanceDto.Amount);

            this.unitOfWork.Wallets.UpdateRange([fromWallet, toWallet]);
            await this.unitOfWork.Transactions.AddAsync(transaction);

            await this.unitOfWork.CommitAsync();

            return ResultData<TransferBalanceResponse>.Success(new TransferBalanceResponse() { TransactionId = transaction.Id }, HttpStatusCode.Created);
        }
    }
}
