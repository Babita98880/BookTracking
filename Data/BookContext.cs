using Microsoft.EntityFrameworkCore;
using Web1001_BookTracking.Models;
namespace Web1001_BookTracking.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
        : base(options)
        { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<CategoryType> CategoryTypes { get; set; } = null!;
          protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                 modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<Category>()
                .HasKey(entity => entity.Id);

            modelBuilder.Entity<CategoryType>()
                .HasKey(entity => entity.Id);

            // Configure the relationship between Category and CategoryType
            modelBuilder.Entity<Category>()
                .HasOne(c => c.CategoryType)
                .WithMany() // No direct relationship between CategoryType and Category
                .HasForeignKey(c => c.TypeId);
        
            }
    }
    
}
