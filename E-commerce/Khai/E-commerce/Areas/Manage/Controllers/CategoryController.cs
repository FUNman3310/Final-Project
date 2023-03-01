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
    public class CategoryController : Controller
	{
		private readonly KhaiContext _khaiContext;

		public CategoryController(KhaiContext khaiContext)
		{
			_khaiContext = khaiContext;
		}
		public IActionResult Index(int page =1)
		{
			var query = _khaiContext.Categories.AsQueryable();
			PaginatedList<Category> paginatedProducts = PaginatedList<Category>.Create(query, 5, page);
			return View(paginatedProducts);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(ProductCharacteristicsViewModel productCharacteristics)
		{
			if (!ModelState.IsValid) return View();

			Category category = new Category
			{
				Name = productCharacteristics.Name
			};

			_khaiContext.Categories.Add(category);
			_khaiContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult Update(int id)
		{
			Category category = _khaiContext.Categories.Find(id);

			if (category == null) return View("Error");

			return View(category);
		}
		[HttpPost]
		public IActionResult Update(int id, ProductCharacteristicsViewModel productCharacteristics)
		{
			Category existCategory = _khaiContext.Categories.Find(id);
			if (existCategory == null) return View("Error");

			if (!ModelState.IsValid) return View();

			existCategory.Name = productCharacteristics.Name;

			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}
		public IActionResult Delete(int id)
		{
			Category category = _khaiContext.Categories.FirstOrDefault(x => x.Id == id);

			if (category == null) return NotFound();

			_khaiContext.Categories.Remove(category);
			_khaiContext.SaveChanges();
			return RedirectToAction("index");

		}
	}
}
