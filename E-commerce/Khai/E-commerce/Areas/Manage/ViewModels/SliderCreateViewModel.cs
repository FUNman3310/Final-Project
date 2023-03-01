using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Areas.Manage.ViewModels
{
	public class SliderCreateViewModel
	{
		public string SubTitle { get; set; }

		[StringLength(maximumLength: 20)]
		public string Title { get; set; }
		

		[NotMapped]
		public IFormFile ImageFile { get; set; }
	}
}
