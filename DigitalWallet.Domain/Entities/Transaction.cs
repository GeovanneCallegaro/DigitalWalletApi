namespace DigitalWallet.Domain.Entities
{
    public class Transaction : IEntity
    {
        public Guid Id { get; init; }
        public Guid SenderWalletId { get; private set; }
        public Guid ReceiverWalletId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Transaction(Guid senderWalletId, Guid receiverWalletId, decimal amount)
        {
            Id = Guid.NewGuid();
            SenderWalletId = senderWalletId;
            ReceiverWalletId = receiverWalletId;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
