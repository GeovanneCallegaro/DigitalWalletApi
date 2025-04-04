namespace DigitalWallet.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITransactionRepository Transactions { get; }
        IWalletRepository Wallets { get; }
        Task<int> CommitAsync();
    }
}
