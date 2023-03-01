using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Areas.Manage.Controllers
{
	public class AllUsersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
