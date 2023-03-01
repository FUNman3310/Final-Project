﻿using System.ComponentModel.DataAnnotations;

namespace E_commerce.Areas.Manage.ViewModels
{
	public class AdminLoginViewModel
	{
		[Required]
		[StringLength(maximumLength: 25)]
		public string Username { get; set; }
		[Required]
		[StringLength(maximumLength: 30, MinimumLength = 8), DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
