using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Areas.Manage.ViewModels
{
	public class ProductCreateViewModel
	{
		public int CategoryId { get; set; }
		public int ProductTypeId { get; set; }
		public int ColorId { get; set; }

		public int MaterialId { get; set; }


		[StringLength(maximumLength: 30, ErrorMessage = "Max length is 30")]
		public string Name { get; set; }
		[StringLength(maximumLength: 10, ErrorMessage = "Max length is 10")]
		public string Code { get; set; }
		[StringLength(maximumLength: 100, ErrorMessage = "Max length is 100")]
		public string ShortDescription { get; set; }
		[StringLength(maximumLength: 200, ErrorMessage = "Max length is 200")]
		public string Description { get; set; }



		public int Count { get; set; }

		public bool IsAvailable { get; set; }

		public double SalePrice { get; set; }

		public double CostPrice { get; set; }

		public int Discount { get; set; }


		public bool IsTranding { get; set; }

		public bool IsRecommend { get; set; }




		[NotMapped]
		public List<IFormFile> ImageFiles { get; set; }

		[NotMapped]
		public IFormFile PosterImageFile { get; set; }
	}
}
