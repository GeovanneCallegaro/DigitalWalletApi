namespace DigitalWallet.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string email, string userId);
    }
}
