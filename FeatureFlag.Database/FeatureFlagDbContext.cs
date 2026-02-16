using Microsoft.EntityFrameworkCore;

namespace FeatureFlag.Database
{
    public class FeatureFlagDbContext : DbContext
    {
        public FeatureFlagDbContext(DbContextOptions<FeatureFlagDbContext> options) : base(options) { }

        public DbSet<FeatureFlag.Domain.Entity.FeatureFlag> FeatureFlags { get; set; }
    }
}
