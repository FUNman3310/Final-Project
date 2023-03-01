using E_commerce.Models;

namespace E_commerce.ViewModels
{
	public class ProductDetailViewModel
	{
		public Product product { get; set; }

		public List<Product> RelatedProducts { get; set; }
	}
}
