using E_commerce.Areas.Manage.ViewModels;
using E_commerce.Helper;
using E_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_commerce.Areas.Manage.Controllers
{
	[Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
	{
		private readonly KhaiContext _khaiContext;
		private readonly IWebHostEnvironment _env;

		public SliderController(KhaiContext	khaiContext, IWebHostEnvironment env)
		{
			_khaiContext = khaiContext;
			_env = env;
		}
		public IActionResult Index()
		{
			return View(_khaiContext.Sliders.ToList());
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(SliderCreateViewModel createViewModel)
		{
			if (!ModelState.IsValid) return View();
			if (createViewModel.ImageFile.ContentType != "image/jpeg" && createViewModel.ImageFile.ContentType != "image/png")
			{
				ModelState.AddModelError("ImageFiles", "Only png and jpeg format!");
				return View();
			}

			Slider slider = new Slider
			{
				Title= createViewModel.Title,
				SubTitle= createViewModel.SubTitle,
				Image = FileManager.SaveFile(_env.WebRootPath, "uploads/slider/", createViewModel.ImageFile)
		    };

			_khaiContext.Sliders.Add(slider);
			_khaiContext.SaveChanges();

			return RedirectToAction("Index");
		}
		public IActionResult Update(int id)
		{
			Slider slider = _khaiContext.Sliders.Find(id);

			if(slider == null) return View("Error");

			return View(slider);
		}
		[HttpPost]
		public IActionResult Update(int id, SliderUpdateViewModel createViewModel)
		{
			Slider existSlider = _khaiContext.Sliders.Find(id);
			if (existSlider == null) return View("Error");

			if (!ModelState.IsValid) return View();

			if (createViewModel.ImageFile != null)
			{
				string name = FileManager.SaveFile(_env.WebRootPath, "uploads/slider/", createViewModel.ImageFile);

				FileManager.DeleteFile(_env.WebRootPath, "uploads/slider/", existSlider.Image);
				existSlider.Image = name;
			}
			
			existSlider.Title = createViewModel.Title;
			existSlider.SubTitle = createViewModel.SubTitle;

			
			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}
		public IActionResult Delete(int id)
		{
			Slider slider = _khaiContext.Sliders.FirstOrDefault(x => x.Id == id);

			if (slider == null) return NotFound();

			_khaiContext.Sliders.Remove(slider);
			FileManager.DeleteFile(_env.WebRootPath, "uploads/slider/", slider.Image);
			_khaiContext.SaveChanges();
			return RedirectToAction("index");
			
		}
	}
}
