using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_commerce.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize(Roles = "Admin")]
	public class СharacteristicsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
