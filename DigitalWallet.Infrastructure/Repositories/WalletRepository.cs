using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Repositories;
using DigitalWallet.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Wallet> GetByUser(Guid userId)
        {
            return await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        }
    }
}
