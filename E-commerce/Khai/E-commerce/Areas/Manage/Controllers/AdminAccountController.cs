using E_commerce.Areas.Manage.ViewModels;
using E_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_commerce.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize(Roles = "Admin")]
	public class AdminAccountController : Controller
	{

		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly KhaiContext _khaiContext;

		public AdminAccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, KhaiContext khaiContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_khaiContext = khaiContext;
		}


		//public IActionResult Login()
		//{
		//	return View();
		//}
		//[HttpPost]
		//public async Task<IActionResult> Login(AdminLoginViewModel loginViewModel)
		//{
		//	if (!ModelState.IsValid) return View();


		//	AppUser user = await _userManager.FindByNameAsync(loginViewModel.Username);
		//	if (user == null)
		//	{
		//		ModelState.AddModelError("", "Wrong Username or Password");
		//		return View();
		//	}

		//	var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
		//	if (!result.Succeeded)
		//	{
		//		ModelState.AddModelError("", "Wrong Username or Password");
		//		return View();
		//	}


		//	return RedirectToAction("index", "dashboard");
		//}


		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(AdminRegisterViewModel registerViewModel)
		{
			if (!ModelState.IsValid) return View();


			AppUser Admin = await _userManager.FindByNameAsync(registerViewModel.Username);
			if (Admin != null)
			{
				ModelState.AddModelError("Username", "Username has taken");
				return View();
			}

			Admin = await _khaiContext.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == registerViewModel.Email.ToUpper());

			if (Admin != null)
			{
				ModelState.AddModelError("Email", "Email has taken");
				return View();
			}

			Admin = new AppUser
			{
				Fullname = registerViewModel.Fullname,
				Email = registerViewModel.Email,
				UserName = registerViewModel.Username,
				IsAdmin = true

			};

			var result = await _userManager.CreateAsync(Admin, registerViewModel.Password);

			if (!result.Succeeded)
			{
				foreach (var err in result.Errors)
				{
					ModelState.AddModelError("", err.Description);
					return View();

				}
			}

			var roleResult = await _userManager.AddToRoleAsync(Admin, "Admin");

			if (!roleResult.Succeeded)
			{
				foreach (var err in roleResult.Errors)
				{
					ModelState.AddModelError("", err.Description);
					return View();

				}
			}

			//await _signInManager.SignInAsync(Admin, isPersistent: false);


			//return Ok(Admin);
			return RedirectToAction("index", "dashboard");
		}

		public async Task<IActionResult> Logout()
		{
			if (User.Identity.IsAuthenticated)
			{
				await _signInManager.SignOutAsync();
			}
			return RedirectToAction("index", "dashboard");
		}
	}
}
