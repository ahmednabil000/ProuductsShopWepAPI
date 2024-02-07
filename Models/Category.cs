using System.ComponentModel.DataAnnotations;

namespace ProuductsShopWepAPI.Models
{
	public class Category
	{
		[Key]
		[Required]
		public long Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
