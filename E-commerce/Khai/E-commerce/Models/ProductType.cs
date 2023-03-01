namespace E_commerce.Models
{
	public class ProductType
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<Product> Products { get; set; }
	}
}
