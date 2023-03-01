using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
	public class MyAccount
	{
		public int Id { get; set; }
		public string AppUserId { get; set; }
		public int CountryId { get; set; }
		[StringLength(maximumLength: 100)]
		public string Address { get; set; }
		
		[StringLength(maximumLength:10,MinimumLength =10)]
		public string Mobile { get; set; }

		public string Image { get; set; }

		[NotMapped]
		public IFormFile imageFile { get; set; }

		public Country? Country { get; set; }
		public AppUser AppUser { get; set; }
	}
}
