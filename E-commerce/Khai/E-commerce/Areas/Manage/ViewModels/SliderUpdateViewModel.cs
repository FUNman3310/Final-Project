using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Areas.Manage.ViewModels
{
	public class SliderUpdateViewModel
	{
		public string SubTitle { get; set; }

		[StringLength(maximumLength: 20)]
		public string Title { get; set; }
		

		[NotMapped]
		public IFormFile? ImageFile { get; set; }
	}
}
