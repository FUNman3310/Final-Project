using System.ComponentModel.DataAnnotations;

namespace E_commerce.ViewModels
{
	public class OrderViewModel
	{
		public List<CheckoutItemViewModel>? checkoutItemViews { get; set; }
		public int CountryId { get; set; }
		[StringLength(maximumLength: 100)]
		public string Address { get; set; }

		[StringLength(maximumLength: 10, MinimumLength = 10)]
		public string Mobile { get; set; }
		public string Note { get; set; }
		[StringLength(maximumLength: 30)]
		public string Fullname { get; set; }
		[StringLength(maximumLength: 35)]
		public string Email { get; set; }
	}
}
