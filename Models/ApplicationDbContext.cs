using Microsoft.EntityFrameworkCore;

namespace ProuductsShopWepAPI.Models
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Write Fluent API configurations here

			//Property Configurations
			modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
			modelBuilder.Entity<Category>().HasKey(c => c.Id);
			modelBuilder.Entity<Category>()
				.Property(c => c.Id)
				.ValueGeneratedNever(); // Specify that the primary key is not generated automatically

		}
	}
}
