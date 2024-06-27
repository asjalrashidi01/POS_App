using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public class CashierContext : DbContext
{
    public CashierContext(DbContextOptions<CashierContext> options)
        : base(options)
    {
    }

    public DbSet<CashierItem> CashierItems { get; set; } = null!;
}