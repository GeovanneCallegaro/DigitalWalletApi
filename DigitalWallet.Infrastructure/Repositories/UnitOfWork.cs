using DigitalWallet.Domain.Repositories;
using DigitalWallet.Infrastructure.Data;

namespace DigitalWallet.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext _context { get; set; }
        public IUserRepository Users { get; }
        public ITransactionRepository Transactions { get; }
        public IWalletRepository Wallets { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Transactions = new TransactionRepository(context);
            Wallets = new WalletRepository(context);
        }

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
