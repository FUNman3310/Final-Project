using System.ComponentModel.DataAnnotations;

namespace E_commerce.Areas.Manage.ViewModels
{
	public class AdminRegisterViewModel
	{
		[Required]
		[StringLength(maximumLength: 20)]
		public string Fullname { get; set; }
		[Required]
		[StringLength(maximumLength: 25)]
		public string Username { get; set; }
		[Required]
		[StringLength(maximumLength: 30)]
		public string Email { get; set; }
		[Required]
		[StringLength(maximumLength: 30, MinimumLength = 8), DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[StringLength(maximumLength: 30, MinimumLength = 8), DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
