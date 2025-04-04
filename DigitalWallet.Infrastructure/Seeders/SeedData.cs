using DigitalWallet.Domain.Entities;
using DigitalWallet.Infrastructure.Data;
using DigitalWallet.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.Seeders
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            if (await context.Users.AnyAsync()) return; // já populado

            var users = new List<User>
            {
                new("Alice", "alice@email.com", PasswordHasher.HashPassword("123456")),
                new("Luandra", "luandra@email.com", PasswordHasher.HashPassword("123456")),
            };

            var wallets = users.Select(u => new Wallet
            {
                Id = Guid.NewGuid(),
                UserId = u.Id,
                Balance = new Random().Next(1000, 10000)
            }).ToList();

            var transactions = new List<Transaction>
            {
                new(wallets[0].Id, wallets[1].Id, 150),
                new(wallets[1].Id, wallets[0].Id, 400)
            };

            context.Users.AddRange(users);
            context.Wallets.AddRange(wallets);
            context.Transactions.AddRange(transactions);

            await context.SaveChangesAsync();
        }
    }
}
