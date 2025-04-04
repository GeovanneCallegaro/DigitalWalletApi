using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Repositories;
using DigitalWallet.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Transaction>> GetUserTransactionsAsync(Guid senderWalletId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transactions
                .AsNoTracking()
                .Where(t => t.SenderWalletId == senderWalletId);

            if (startDate.HasValue)
                query = query.Where(t => t.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.CreatedAt <= endDate.Value);

            return await query.ToListAsync();
        }
    }
}
