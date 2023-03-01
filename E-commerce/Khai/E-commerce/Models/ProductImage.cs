using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
	public class ProductImage
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string Image { get; set; }
		public bool IsPoster { get; set; }
		public Product Product { get; set; }
	}
}
