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
        public DbSet<CategoryType> categoryTypes { get; set; } = null!;
    }
}
