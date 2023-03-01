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
    public class MaterialController : Controller
	{
		private readonly KhaiContext _khaiContext;

		public MaterialController(KhaiContext khaiContext)
		{
			_khaiContext = khaiContext;
		}

		public IActionResult Index(int page=1)
		{
			var query = _khaiContext.Materials.AsQueryable();
			PaginatedList<Material> paginatedMaterial = PaginatedList<Material>.Create(query, 5, page);
			return View(paginatedMaterial);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(ProductCharacteristicsViewModel productCharacteristics)
		{
			if (!ModelState.IsValid) return View();

			Material material = new Material
			{
				Name = productCharacteristics.Name
			};

			_khaiContext.Materials.Add(material);
			_khaiContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult Update(int id)
		{
			Material material = _khaiContext.Materials.Find(id);

			if (material == null) return View("Error");

			return View(material);
		}
		[HttpPost]
		public IActionResult Update(int id, ProductCharacteristicsViewModel productCharacteristics)
		{
			Material existMaterial = _khaiContext.Materials.Find(id);
			if (existMaterial == null) return View("Error");

			if (!ModelState.IsValid) return View();

			existMaterial.Name = productCharacteristics.Name;

			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}
		public IActionResult Delete(int id)
		{
			Material material = _khaiContext.Materials.FirstOrDefault(x => x.Id == id);

			if (material == null) return NotFound();

			_khaiContext.Materials.Remove(material);
			_khaiContext.SaveChanges();
			return RedirectToAction("index");

		}
	}
}
