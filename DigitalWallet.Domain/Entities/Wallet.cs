namespace DigitalWallet.Domain.Entities
{
    public class Wallet()
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public decimal Balance { get; set; } = 0;

        public Guid UserId { get; set; }
        public virtual User User { get; set; } 
    }
}
