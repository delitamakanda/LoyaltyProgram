using Microsoft.EntityFrameworkCore;
using LoyaltyProgram.Domain;


namespace LoyaltyProgram.Infrastructure
{
    public class LoyaltyDbContext : DbContext
    {
        public LoyaltyDbContext(DbContextOptions<LoyaltyDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<HistoryReward> HistoryRewards { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<RankSystem> RankSystems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
            .HasOne(c => c.LoyaltyCard)
            .WithOne()
            .HasForeignKey<LoyaltyCard>("clientId");

            modelBuilder.Entity<RankSystem>()
            .Property(r => r.Rank)
            .HasConversion<string>();

            modelBuilder.Entity<LoyaltyCard>()
            .Property(c => c.Rank)
            .HasConversion<string>();

            modelBuilder.Entity<LoyaltyCard>()
            .Property(c => c.Status)
            .HasConversion<string>();

            modelBuilder.Entity<LoyaltyCard>()
            .HasMany(c => c.Transactions)
            .WithOne();

            modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Shop);

            modelBuilder.Entity<Transaction>()
            .Property(t => t.TransactionType)
            .HasConversion<string>();

            modelBuilder.Entity<HistoryReward>()
            .HasOne(hr => hr.Reward);

            modelBuilder.Entity<HistoryReward>()
            .HasOne(hr => hr.Client);
        }

    }
}
