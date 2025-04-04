namespace DigitalWallet.Application.DTOs.Transaction
{
    public class GetTransactionsByUserDto
    {
        public Guid UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
