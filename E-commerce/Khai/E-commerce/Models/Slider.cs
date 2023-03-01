using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
	public class Slider
	{
		public int Id { get; set; }
		[StringLength(maximumLength:20)]
		public string SubTitle { get; set; }

		[StringLength(maximumLength:20)]
		public string Title { get; set; }

		public string Image { get; set;}
		[NotMapped]
		public IFormFile ImageFile { get; set; }
	}
}
