namespace TheProject.Core
{
    using Microsoft.EntityFrameworkCore;
    using Entities;

    public abstract class AppBaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }

        public AppBaseContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
