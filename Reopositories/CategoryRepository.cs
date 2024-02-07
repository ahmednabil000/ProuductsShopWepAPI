using Microsoft.EntityFrameworkCore;
using ProuductsShopWepAPI.Models;
using ProuductsShopWepAPI.RepositoriesContracts;

namespace ProuductsShopWepAPI.Reopositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _context;
		public CategoryRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task AddCategoriesAsync(IList<Category> categories)
		{
			if (categories != null)
			{
				await _context.Categories.AddRangeAsync(categories);
			}
			await _context.SaveChangesAsync();
		}

		public IQueryable<Category> GetAll()
		{
			return _context.Categories.AsNoTracking().AsQueryable();
		}
	}
}
