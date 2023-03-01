using E_commerce.ViewModels;
using E_commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using E_commerce.Helper;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using E_commerce.Abstractions.MailService;
//using E_commerce.ViewModels.MailSender;

namespace E_commerce.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly KhaiContext _khaiContext;
		//private readonly IMailService _mailService;



		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, KhaiContext khaiContext/*, IMailService mailService*/)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_khaiContext = khaiContext;
			//_mailService = mailService;
		}


		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(MemberLoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid) return View();


			AppUser user = await _userManager.FindByNameAsync(loginViewModel.Username);
			if (user == null)
			{
				ModelState.AddModelError("", "Wrong Username or Password");
				return View();
			}

			var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "Wrong Username or Password");
				return View();
			}


			return RedirectToAction("index", "home");
		}


		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(MemberRegisterViewModel registerViewModel)
		{
			if (!ModelState.IsValid) return View();



			AppUser Member = await _userManager.FindByNameAsync(registerViewModel.Username);
			if (Member != null)
			{
				ModelState.AddModelError("Username", "Username has taken");
				return View();
			}

			Member = await _khaiContext.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == registerViewModel.Email.ToUpper());

			if (Member != null)
			{
				ModelState.AddModelError("Email", "Email has taken");
				return View();
			}

			Member = new AppUser
			{
				Fullname = registerViewModel.Fullname,
				Email = registerViewModel.Email,
				UserName = registerViewModel.Username,
				IsAdmin = false,
				
				
			};

			var result = await _userManager.CreateAsync(Member, registerViewModel.Password);

			if (!result.Succeeded)
			{
				foreach (var err in result.Errors)
				{
					ModelState.AddModelError("", err.Description);
					return View();

				}
			}

			var roleResult = await _userManager.AddToRoleAsync(Member, "Member");

			if (!roleResult.Succeeded)
			{
				foreach (var err in roleResult.Errors)
				{
					ModelState.AddModelError("", err.Description);
					return View();

				}
			}

			await _signInManager.SignInAsync(Member, isPersistent: false);

			//return Ok(Admin);
			return RedirectToAction("index", "home");
		}

		public async Task<IActionResult> Logout()
		{
			if (User.Identity.IsAuthenticated)
			{
				await _signInManager.SignOutAsync();
			}
			return RedirectToAction("index", "home");
		}

		//public IActionResult MyAccount()
		//{
		//	ViewBag.Country = _khaiContext.Countries.ToList();



			//	return View();
			//}

			//public IActionResult ForgotPassword()
			//{
			//	return View();
			//}
			//[HttpPost]
			//[ValidateAntiForgeryToken]
			//public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordView)
			//{
			//	if (!ModelState.IsValid) return View(forgotPasswordView);
			//	var user = await _userManager.FindByEmailAsync(forgotPasswordView.Email);
			//	if(user == null) return View(forgotPasswordView);
			//	string token = await _userManager.GeneratePasswordResetTokenAsync(user);
			//	string link = Url.Action("ResetPassword", "Account", new {userId=user.Id,token=token},HttpContext.Request.Scheme);
			//	await _mailService.SendEmailAsync(new MailRequestVM { ToEmail=forgotPasswordView.Email,Subject="Reset Password",Body=$"<a href='{ link}'>Reset Password</a>"});
			//	return RedirectToAction(nameof(Login));
			//}
			//public async Task<IActionResult> ResetPassword(string userId,string token)
			//{
			//	if(string.IsNullOrWhiteSpace(userId)||string.IsNullOrWhiteSpace(token)) return BadRequest();
			//	var user = await _userManager.FindByIdAsync(userId);
			//	if(user == null) return NotFound();
			//	return View();
			//}
			//[HttpPost]
			//public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordView,string userId,string token)
			//{
			//	if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();
			//	if (!ModelState.IsValid) return View(resetPasswordView);
			//	var user = await _userManager.FindByIdAsync(userId);
			//	if (user == null) return NotFound();
			//	var identityUser = await _userManager.ResetPasswordAsync(user, token, resetPasswordView.ConfirmPassword);
			//	return RedirectToAction(nameof(Login));
			//}


		}
}
