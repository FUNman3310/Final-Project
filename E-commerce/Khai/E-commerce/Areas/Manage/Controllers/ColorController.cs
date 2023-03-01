using E_commerce.Areas.Manage.ViewModels;
using E_commerce.Helper;
using E_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace E_commerce.Areas.Manage.Controllers
{
	[Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class ColorController : Controller
	{
		private readonly KhaiContext _khaiContext;

		public ColorController(KhaiContext khaiContext)
		{
			_khaiContext = khaiContext;
		}

		public IActionResult Index(int page =1 )
		{
			var query = _khaiContext.Colors.AsQueryable();
			PaginatedList<Color> paginatedColors = PaginatedList<Color>.Create(query, 5, page);
			return View(paginatedColors);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(ProductCharacteristicsViewModel productCharacteristics)
		{
			if (!ModelState.IsValid) return View();

			Color color = new Color
			{
				Name = productCharacteristics.Name
			};

			_khaiContext.Colors.Add(color);
			_khaiContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult Update(int id)
		{
			Color color = _khaiContext.Colors.Find(id);

			if (color == null) return View("Error");

			return View(color);
		}
		[HttpPost]
		public IActionResult Update(int id, ProductCharacteristicsViewModel productCharacteristics)
		{
			Color existColor = _khaiContext.Colors.Find(id);
			if (existColor == null) return View("Error");

			if (!ModelState.IsValid) return View();

			existColor.Name = productCharacteristics.Name;

			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}
		public IActionResult Delete(int id)
		{
			Color color = _khaiContext.Colors.FirstOrDefault(x => x.Id == id);

			if (color == null) return NotFound();

			_khaiContext.Colors.Remove(color);
			_khaiContext.SaveChanges();
			return RedirectToAction("index");
		}
	}
}
