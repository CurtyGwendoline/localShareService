using Microsoft.EntityFrameworkCore;

namespace localShareService.Model
{
    public class LocalShareCtx : DbContext
    {
        public LocalShareCtx(DbContextOptions<LocalShareCtx> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

    }
}
