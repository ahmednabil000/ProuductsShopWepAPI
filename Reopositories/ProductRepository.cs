using Microsoft.EntityFrameworkCore;
using ProuductsShopWepAPI.Models;
using ProuductsShopWepAPI.RepositoriesContracts;

namespace ProuductsShopWepAPI.Reopositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;
		public ProductRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public IQueryable<Product> GetAll()
		{
			return _context.Products.AsNoTracking().AsQueryable();
		}
		public Task AddProductAsync(Product product)
		{
			throw new NotImplementedException();
		}
		public async Task AddProductsAsync(IList<Product> products)
		{
			if (products != null)
			{

				await _context.Products.AddRangeAsync(products);
			}
			await _context.SaveChangesAsync();

		}

		public async Task<bool> IsProductExistAsync(string productId)
		{
			var prod = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == productId);
			if (prod == null) return false;
			return true;
		}
	}
}
