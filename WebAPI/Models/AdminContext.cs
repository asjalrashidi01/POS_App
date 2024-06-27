using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public class AdminContext : DbContext
{
    public AdminContext(DbContextOptions<AdminContext> options)
        : base(options)
    {
    }

    public DbSet<AdminItem> AdminItems { get; set; } = null!;
}