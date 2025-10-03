using Microsoft.EntityFrameworkCore;
using BankingManagementSystem.Models;

namespace BankingManagementSystem.Data
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Account entity
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId);
                entity.Property(e => e.Balance).HasPrecision(18, 2);
                entity.HasIndex(e => e.AccountNumber).IsUnique();
            });

            // Seed initial data
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountId = 1,
                    AccountNumber = "ACC001",
                    AccountHolderName = "John Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "1234567890",
                    AccountType = "Savings",
                    Balance = 5000.00M,
                    DateOpened = DateTime.Now.AddMonths(-6),
                    IsActive = true
                },
                new Account
                {
                    AccountId = 2,
                    AccountNumber = "ACC002",
                    AccountHolderName = "Jane Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "0987654321",
                    AccountType = "Current",
                    Balance = 10000.00M,
                    DateOpened = DateTime.Now.AddMonths(-3),
                    IsActive = true
                }
            );
        }
    }
}