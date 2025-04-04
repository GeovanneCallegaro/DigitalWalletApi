namespace DigitalWallet.Infrastructure.Configuration
{
    public class AppSettings
    {
        public DatabaseOptions Database { get; set; } = new DatabaseOptions();
        public JwtOptions Jwt { get; set; } = new JwtOptions();
    }
}
