using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.ViewModels
{
	public class MyAccountViewModel
	{
		public int CountryId { get; set; }
		[StringLength(maximumLength: 100)]
		public string Address { get; set; }
		[StringLength(maximumLength: 10, MinimumLength = 10)]
		public string Mobile { get; set; }
		[StringLength(maximumLength: 20)]
		public string Fullname { get; set; }
		[NotMapped]
		public IFormFile imageFile { get; set; }
	}
}
