using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;

namespace SigaIdeia.FeedRssAnalytics.Infra.Data.Mappings
{
    public class ArticleMatrixMap : IEntityTypeConfiguration<ArticleMatrix>
    {
        public void Configure(EntityTypeBuilder<ArticleMatrix> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.AuthorId).HasColumnType("varchar(100)").HasColumnName("AuthorId").IsRequired();
            builder.Property(e => e.Author).HasColumnType("varchar(150)").IsRequired();
            builder.Property(e => e.Link).HasColumnType("varchar(max)").IsRequired();
            builder.Property(e => e.Title).HasColumnType("varchar(300)").IsRequired();
            builder.Property(e => e.Type).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.Category).HasColumnType("varchar(150)").IsRequired();
            builder.Property(e => e.Views).HasColumnType("varchar(max)");
        }
    }
}
