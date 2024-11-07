using Microsoft.EntityFrameworkCore;
using WishList.Models;

namespace WishList.ApplicationDbContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}

	public DbSet<Wish> Wishes { get; set; }
    public DbSet<User> Users { get; set; }
}
