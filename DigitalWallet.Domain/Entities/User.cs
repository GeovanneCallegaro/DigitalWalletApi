
namespace DigitalWallet.Domain.Entities
{
    public class User : IEntity
    {
        public User(string name, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            InitializeWallet();
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }

        public virtual Wallet Wallet { get; private set; }

        private void InitializeWallet()
        {
            Wallet = new Wallet();
        }
    }
}
