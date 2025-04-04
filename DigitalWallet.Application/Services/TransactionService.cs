using System.Net;

using DigitalWallet.Application.Common;
using DigitalWallet.Application.DTOs.Transaction;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Domain.Repositories;

using FluentValidation;

namespace DigitalWallet.Application.Services
{
    public class TransactionService(IUnitOfWork unitOfWork, IValidator<GetTransactionsByUserDto> validator) : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IValidator<GetTransactionsByUserDto> _validator = validator;

        public async Task<ResultData<List<GetTransactionsByUserResponse>>> GetUserTransactions(GetTransactionsByUserDto transactionsByUserDto)
        {
            var validationResult = await _validator.ValidateAsync(transactionsByUserDto);
            if (!validationResult.IsValid)
                return ResultData<List<GetTransactionsByUserResponse>>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), HttpStatusCode.BadRequest);

            var wallet = await _unitOfWork.Wallets.GetByUser(transactionsByUserDto.UserId);

            var transactions = await _unitOfWork.Transactions.GetUserTransactionsAsync(wallet.Id, transactionsByUserDto.StartDate, transactionsByUserDto.EndDate);

            var walletIds = transactions.Select(t => t.ReceiverWalletId).Distinct().ToList();

            var wallets = await _unitOfWork.Wallets.GetByIdsAsync(walletIds);

            var walletToUserMap = wallets.ToDictionary(w => w.Id, w => w.UserId);

            var response = transactions.Select(t => new GetTransactionsByUserResponse
            {
                TransactionId = t.Id,
                FromUserId = transactionsByUserDto.UserId,
                ToUserId = walletToUserMap[t.ReceiverWalletId],
                Amount = t.Amount,
                CreatedAt = t.CreatedAt
            }).ToList();

            return ResultData<List<GetTransactionsByUserResponse>>.Success(response, HttpStatusCode.OK);
        }
    }
}
