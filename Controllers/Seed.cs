using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProuductsShopWepAPI.Models;
using ProuductsShopWepAPI.Models.CSV;
using ProuductsShopWepAPI.RepositoriesContracts;
using System.Globalization;

namespace ProuductsShopWepAPI.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class Seed : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;
		private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly ApplicationDbContext _applicationDbContext;
		public Seed(IWebHostEnvironment environment, IProductRepository productRepository, ICategoryRepository categoryRepository, ApplicationDbContext applicationDbContext)
		{
			_environment = environment;
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_applicationDbContext = applicationDbContext;
		}
		[HttpPost]
		public async Task<IActionResult> Products()
		{
			var newProducts = new List<Product>();
			using (var reader = new StreamReader($"{_environment.ContentRootPath}/Data/amazon_products.csv"))
			{
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					var products = csv.GetRecordsAsync<AmazonProduct>();

					var dic1 = _productRepository.GetAll();
					var dic11 = await dic1.ToDictionaryAsync(p => p.ProductId);

					if (products != null)
					{
						int i = 0;
						await foreach (var product in products)
						{
							if (i > 500000) break;
							if (!dic11.ContainsKey(product.asin))
							{
								var newProduct = new Product();
								newProduct.ProductId = product.asin;
								newProduct.BoughtLastMonth = product.boughtInLastMonth;
								newProduct.ListPrice = product.listPrice;
								newProduct.Price = product.price;
								newProduct.Stars = product.stars;
								newProduct.CategoryId = long.Parse(product.category_id);
								newProduct.ImgUrl = product.imgUrl;
								newProduct.Title = product.title;
								newProduct.Reviews = product.reviews;
								if (product.isBestSeller.ToLower() == "true")
								{
									newProduct.IsBestSeller = true;
								}
								else
								{
									newProduct.IsBestSeller = false;
								}
								newProducts.Add(newProduct);
								i++;
							}

						}
					}
					if (newProducts != null && newProducts.Count > 0)
					{
						await _productRepository.AddProductsAsync(newProducts);
					}
				}
			}
			return StatusCode(StatusCodes.Status200OK, $"{newProducts?.Count} products are added");
		}
		[HttpPost]
		public async Task<IActionResult> Categories()
		{
			var newCategoriees = new List<Category>();
			using (var reader = new StreamReader($"{_environment.ContentRootPath}/Data/amazon_categories.csv"))
			{
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					var dic = _categoryRepository.GetAll();
					var dic1 = await dic.ToDictionaryAsync(c => c.Id);
					var categories = csv.GetRecordsAsync<ProductCategorie>();
					await foreach (var category in categories)
					{
						if (!dic1.ContainsKey(long.Parse(category.id)))
						{
							var newCategory = new Category();
							newCategory.Id = long.Parse(category.id);
							newCategory.Name = category.category_name;
							newCategoriees.Add(newCategory);
						}
					}
				}
			}
			await _categoryRepository.AddCategoriesAsync(newCategoriees);
			return StatusCode(StatusCodes.Status200OK, $"{newCategoriees.Count} categories has been added");

		}
		[HttpPost]
		public void pp()
		{
			_applicationDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Categories ON");
			_applicationDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products ON");
		}
	}
}
