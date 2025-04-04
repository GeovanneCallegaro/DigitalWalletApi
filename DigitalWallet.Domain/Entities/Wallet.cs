namespace DigitalWallet.Domain.Entities
{
    public class Wallet() : IEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public decimal Balance { get; set; } = 0;

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public void Deposit(decimal amount) => Balance += amount;

        public void Withdraw(decimal amount) => Balance -= amount;

        public void TransferTo(Wallet destination, decimal amount)
        {
            Withdraw(amount);
            destination.Deposit(amount);
        }
    }
}
