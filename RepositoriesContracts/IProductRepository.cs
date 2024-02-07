using ProuductsShopWepAPI.Models;

namespace ProuductsShopWepAPI.RepositoriesContracts
{
	public interface IProductRepository
	{
		IQueryable<Product> GetAll();
		Task AddProductAsync(Product product);
		Task AddProductsAsync(IList<Product> products);
		Task<bool> IsProductExistAsync(string productId);
	}
}
