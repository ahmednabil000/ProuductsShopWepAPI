namespace ProuductsShopWepAPI.Models.CSV
{
	public record AmazonProduct(
		string asin,
		string title,
		string imgUrl,
		string productURL,
		float stars,
		long reviews,
		decimal price,
		decimal listPrice,
		string category_id,
		string isBestSeller,
		long boughtInLastMonth
		);


}
