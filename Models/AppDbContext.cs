using Microsoft.EntityFrameworkCore;

namespace EduAdmissionSite.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles => Set<Article>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Articles");
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Title)
                      .IsRequired()
                      .HasMaxLength(300);

                entity.Property(a => a.Content)
                      .HasColumnType("nvarchar(max)");

                entity.Property(a => a.ThumbnailUrl)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(a => a.VideoUrl)
                      .HasMaxLength(1000);

                entity.Property(a => a.Category)
                      .HasConversion<int>()
                      .IsRequired();

                entity.Property(a => a.PublishedAt)
                      .HasColumnType("datetime2");
            });
        }
    }
}


