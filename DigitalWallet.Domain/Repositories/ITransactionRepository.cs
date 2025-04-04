using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Domain.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        public Task<List<Transaction>> GetUserTransactionsAsync(Guid senderWalletId, DateTime? startDate, DateTime? endDate);
    }
}
