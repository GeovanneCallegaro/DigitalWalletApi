namespace DigitalWallet.Application.DTOs.Wallet
{
    public class AddBalanceToUserResponse
    {
        public decimal NewBalance { get; set; }
        public Guid UserId { get; set; }
    }
}
