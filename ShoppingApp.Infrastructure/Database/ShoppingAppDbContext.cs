using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Infrastructure.Database;

public class ShoppingAppDbContext : DbContext
{
    public DbSet<Product> Products { get; private set; }

    public ShoppingAppDbContext(DbContextOptions<ShoppingAppDbContext> options)
		: base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoppingAppDbContext).Assembly);
    }
}
