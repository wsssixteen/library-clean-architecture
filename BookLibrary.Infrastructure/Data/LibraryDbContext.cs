using BookLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configuration instead of EF Core's data annotation
            // Clean & explicit
            modelBuilder.Entity<Book>(builder =>
            {
                builder.HasKey(builder => builder.Id);
                builder.Property(b => b.Title)
                        .IsRequired()
                        .HasMaxLength(200);
                builder.Property(b => b.Author)
                        .IsRequired()
                        .HasMaxLength(200);
                builder.Property(b => b.Price)
                        .HasColumnType("decimal(18,2)");
            });
        }
    }
}
