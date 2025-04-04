namespace DigitalWallet.Application.DTOs.Wallet
{
    public class TransferBalanceDto
    {
        public Guid ReceiverUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
