using APTask.Entities;
using APTask.Entities.DefaultData;
using Microsoft.EntityFrameworkCore;

namespace APTask.DAL
{
    public class APTaskDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public APTaskDbContext(DbContextOptions<APTaskDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x => x.Id);

            modelBuilder
                .Entity<Category>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder
                .Entity<Product>()
                .Property(x => x.ProductCode)
                .HasMaxLength(50);
            //I assume that ProductCode must be unique
            modelBuilder
                .Entity<Product>()
                .HasIndex(x => x.ProductCode)
                .IsUnique();

            modelBuilder.Entity<User>().HasData(DefaultUsers.Users);
        }
    }
}
