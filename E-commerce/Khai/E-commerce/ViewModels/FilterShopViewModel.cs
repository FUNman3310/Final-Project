using E_commerce.Helper;
using E_commerce.Models;

namespace E_commerce.ViewModels
{
	public class FilterShopViewModel
	{
		public List<Product> FilterProducts { get; set; }

		public IndexerEnum indexer { get; set; }

		public List<Category> Categories { get; set; }
		public List<ProductType> Types { get; set; }
		public List<Color> Colors { get; set; }
		public List<Material> Materials { get; set; }
	}
}
