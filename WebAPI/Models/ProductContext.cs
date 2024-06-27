using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options)
        : base(options)
    {
    }

    public DbSet<ProductItem> ProductItems { get; set; } = null!;
}