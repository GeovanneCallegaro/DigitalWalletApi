using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Domain.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Task<Wallet> GetByUser(Guid userId);
    }
}
