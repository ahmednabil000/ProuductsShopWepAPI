using System.ComponentModel.DataAnnotations;

namespace ProuductsShopWepAPI.Models
{
	public class Product
	{
		[Key]
		[Required]
		public string ProductId { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string ImgUrl { get; set; }
		[Required]
		public float Stars { get; set; }
		[Required]
		public float Reviews { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public decimal ListPrice { get; set; }
		[Required]
		public bool IsBestSeller { get; set; }
		[Required]
		public long BoughtLastMonth { get; set; }
		[Required]
		public long CategoryId { get; set; }

		public Category Category { get; set; }
	}
}
