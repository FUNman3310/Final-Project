using E_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_commerce.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize(Roles = "Admin")]
	public class MainPageController : Controller
	{
		private readonly KhaiContext _khaiContext;

		public MainPageController(KhaiContext khaiContext)
		{
			_khaiContext = khaiContext;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About(int id)
		{
			AboutPage aboutPage = _khaiContext.aboutPage.Find(id);

			if (aboutPage == null) return View("Error");

			return View(aboutPage);
		}
		[HttpPost]
		public IActionResult About(AboutPage aboutPage)
		{
			//if (!ModelState.IsValid) return View(categoryType);
			AboutPage existAbout = _khaiContext.aboutPage.Find(aboutPage.Id);
			if (existAbout == null) return View("Error");

			existAbout.Description = aboutPage.Description;

			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}



		public IActionResult Refund(int id)
		{
			RefundPage refundPage = _khaiContext.refundPage.Find(id);

			if (refundPage == null) return View("Error");

			return View(refundPage);
		}
		[HttpPost]
		public IActionResult Refund(RefundPage refundPage)
		{
			//if (!ModelState.IsValid) return View(categoryType);
			RefundPage existRefund = _khaiContext.refundPage.Find(refundPage.Id);
			if (existRefund == null) return View("Error");

			existRefund.Description = refundPage.Description;

			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}




		public IActionResult Terms(int id)
		{
			TermsPage termsPage = _khaiContext.termsPage.Find(id);

			if (termsPage == null) return View("Error");

			return View(termsPage);
		}
		[HttpPost]
		public IActionResult Terms(TermsPage termsPage)
		{
			//if (!ModelState.IsValid) return View(categoryType);
			TermsPage existTerms = _khaiContext.termsPage.Find(termsPage.Id);
			if (existTerms == null) return View("Error");

			existTerms.Description = termsPage.Description;

			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}
	}
}
