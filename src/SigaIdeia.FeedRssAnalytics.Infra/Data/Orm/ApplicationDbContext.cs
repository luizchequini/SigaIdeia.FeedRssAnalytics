using Microsoft.EntityFrameworkCore;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;

namespace SigaIdeia.FeedRssAnalytics.Infra.Data.Orm
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ArticleMatrix> ArticleMatrix { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
