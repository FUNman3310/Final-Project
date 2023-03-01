using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string? AppUserId { get; set; }
		[StringLength(maximumLength: 35)]
		public string Email { get; set; }
		public int CountryId { get; set; }
		[StringLength(maximumLength: 100)]
		public string Address { get; set; }
		[StringLength(maximumLength: 30)]
		public string FullName { get; set; }
		[StringLength(maximumLength: 10, MinimumLength = 10)]
		public string Mobile { get; set; }
		public string Note { get; set; }
		public double TotalCost { get; set; }
		public DateTime CreateTime { get; set; }
		public Country? Country { get; set; }
		public AppUser AppUser { get; set; }
		public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	}
}
