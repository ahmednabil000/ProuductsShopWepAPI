using ProuductsShopWepAPI.Models;

namespace ProuductsShopWepAPI.RepositoriesContracts
{
	public interface ICategoryRepository
	{
		IQueryable<Category> GetAll();
		Task AddCategoriesAsync(IList<Category> categories);
	}
}
