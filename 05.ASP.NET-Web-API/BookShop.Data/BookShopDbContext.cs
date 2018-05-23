namespace BookShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BookShopDbContext : DbContext
    {
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryBooks> BooksInCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<CategoryBooks>()
                .HasKey(bc => new { bc.CategoryId, bc.BookId });

            builder
                .Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            builder
                .Entity<Book>()
                .HasMany(b => b.Categories)
                .WithOne(cb => cb.Book)
                .HasForeignKey(b => b.BookId);

            builder
                .Entity<Category>()
                .HasMany(c => c.Books)
                .WithOne(cb => cb.Category)
                .HasForeignKey(c => c.CategoryId);

            base.OnModelCreating(builder);
        }
    }
}
