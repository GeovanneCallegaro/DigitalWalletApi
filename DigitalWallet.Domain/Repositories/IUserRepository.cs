using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
