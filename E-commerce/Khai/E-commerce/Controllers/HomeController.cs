using E_commerce.Models;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace E_commerce.Controllers
{

    public class HomeController : Controller
    {
		private readonly KhaiContext _khaiContext;

		public HomeController(KhaiContext khaiContext)
        {
			_khaiContext = khaiContext;
		}
        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                sliders = _khaiContext.Sliders.ToList(),
                IsRecommend = _khaiContext.Products.Include(x=>x.Images).Include(x=>x.ProductType).Include(x=>x.Category).Where(x=>x.IsRecommend).ToList(),
                IsTranding = _khaiContext.Products.Include(x=>x.Images).Include(x=>x.ProductType).Include(x=>x.Category).Where(x=>x.IsTranding).ToList()
            };


            return View(homeVM);
        }

        public IActionResult About()
        {
            return View(_khaiContext.aboutPage.ToList());
        }
		public IActionResult Refund()
		{
			return View(_khaiContext.refundPage.ToList());
		}
		public IActionResult Terms()
		{
			return View(_khaiContext.termsPage.ToList());
		}


        

        
	}
}