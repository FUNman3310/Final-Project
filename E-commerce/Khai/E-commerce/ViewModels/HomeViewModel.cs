using E_commerce.Models;

namespace E_commerce.ViewModels
{
	public class HomeViewModel
	{
		public List<Slider> sliders { get; set; }

		public List<Product> IsRecommend { get; set; }

		public List<Product> IsTranding { get; set; }
	}
}
