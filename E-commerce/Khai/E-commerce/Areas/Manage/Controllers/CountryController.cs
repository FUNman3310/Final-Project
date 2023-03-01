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
	public class CountryController : Controller
	{
		private readonly KhaiContext _khaiContext;

		public CountryController(KhaiContext khaiContext)
		{
			_khaiContext = khaiContext;
		}
		public IActionResult Index(int page = 1)
		{
			var query = _khaiContext.Countries.AsQueryable();
			PaginatedList<Country> paginatedCountry = PaginatedList<Country>.Create(query, 5, page);
			return View(paginatedCountry);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(ProductCharacteristicsViewModel productCharacteristics)
		{
			if (!ModelState.IsValid) return View();

			Country country = new Country
			{
				Name = productCharacteristics.Name
			};

			_khaiContext.Countries.Add(country);
			_khaiContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult Update(int id)
		{
			Country country = _khaiContext.Countries.Find(id);

			if (country == null) return View("Error");

			return View(country);
		}
		[HttpPost]
		public IActionResult Update(int id, ProductCharacteristicsViewModel productCharacteristics)
		{
			Country existCountry = _khaiContext.Countries.Find(id);
			if (existCountry == null) return View("Error");

			if (!ModelState.IsValid) return View();

			existCountry.Name = productCharacteristics.Name;

			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}
		public IActionResult Delete(int id)
		{
			Country country = _khaiContext.Countries.FirstOrDefault(x => x.Id == id);

			if (country == null) return NotFound();

			_khaiContext.Countries.Remove(country);
			_khaiContext.SaveChanges();
			return RedirectToAction("index");
		}
	}
}
