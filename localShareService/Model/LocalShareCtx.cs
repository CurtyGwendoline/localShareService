namespace localShareService.Model
{
    using Microsoft.EntityFrameworkCore;

    public class LocalShareCtx : DbContext
    {
        public LocalShareCtx(DbContextOptions<LocalShareCtx> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure AzureId as the Primary Key using Fluent API
            modelBuilder.Entity<User>()
                .HasKey(u => u.AzureId);
        }
    }
}