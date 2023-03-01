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
    public class ProductTypeController : Controller
	{
		private readonly KhaiContext _khaiContext;

		public ProductTypeController(KhaiContext khaiContext)
		{
			_khaiContext = khaiContext;
		}
		public IActionResult Index(int page = 1)
		{
			var query = _khaiContext.Types.AsQueryable();
			PaginatedList<ProductType> paginatedType = PaginatedList<ProductType>.Create(query, 5, page);
			return View(paginatedType);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(ProductCharacteristicsViewModel productCharacteristics)
		{
			if (!ModelState.IsValid) return View();

			ProductType productType = new ProductType
			{
				Name = productCharacteristics.Name
			};

			_khaiContext.Types.Add(productType);
			_khaiContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult Update(int id)
		{
			ProductType productType = _khaiContext.Types.Find(id);

			if (productType == null) return View("Error");

			return View(productType);
		}
		[HttpPost]
		public IActionResult Update(int id, ProductCharacteristicsViewModel productCharacteristics)
		{
			ProductType existType = _khaiContext.Types.Find(id);
			if (existType == null) return View("Error");

			if (!ModelState.IsValid) return View();

			existType.Name = productCharacteristics.Name;

			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}
		public IActionResult Delete(int id)
		{
			ProductType categoryType = _khaiContext.Types.FirstOrDefault(x => x.Id == id);

			if (categoryType == null) return NotFound();

			_khaiContext.Types.Remove(categoryType);
			_khaiContext.SaveChanges();
			return RedirectToAction("index");

		}
	}
}
