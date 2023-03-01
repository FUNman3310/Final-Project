using E_commerce.Helper;
using E_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace E_commerce.Areas.Manage.Controllers
{
    [Area("Manage")]
	[Authorize(Roles = "Admin")]
	public class DashboardController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly KhaiContext _khaiContext;

		public DashboardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,KhaiContext khaiContext)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_khaiContext = khaiContext;
		}
		public IActionResult Index(int page =1)
        {
			var query = _khaiContext.orders.AsQueryable();
			PaginatedList<Order> paginatedOrder = PaginatedList<Order>.Create(query, 5, page);
			return View(paginatedOrder);
		}


		//public async Task<IActionResult> CreateRole()
		//{
		//	IdentityRole Role1 = new IdentityRole("Admin");
		//	IdentityRole Role2 = new IdentityRole("Member");

		//	await _roleManager.CreateAsync(Role1);
		//	await _roleManager.CreateAsync(Role2);

		//	return Ok("Created");
		//}
	}
}
