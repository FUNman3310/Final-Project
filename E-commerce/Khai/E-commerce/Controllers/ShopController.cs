using E_commerce.Helper;
using E_commerce.Models;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
	public class ShopController : Controller
	{
		private readonly KhaiContext _khaiContext;

		public ShopController(KhaiContext khaiContext)
		{
			_khaiContext = khaiContext;
		}
		public IActionResult Index()
		{
			ShopViewModel shopView = new ShopViewModel
			{
				MainProducts = _khaiContext.Products.Include(x => x.Category).Include(x => x.ProductType).Include(x => x.Images).ToList(),
				Colors = _khaiContext.Colors.ToList(),
				Categories = _khaiContext.Categories.ToList(),
				Materials = _khaiContext.Materials.ToList(),
				Types = _khaiContext.Types.ToList()
		    };

			return View(shopView);
		}

		public IActionResult Filter(int id, IndexerEnum index )
		{
			List<Product> Products = new List<Product>();
			if (index == IndexerEnum.Type)
			{
				Products = _khaiContext.Products.Include(x => x.Category).Include(x => x.ProductType).Include(x => x.Images).Where(x => x.ProductTypeId == id).ToList();
			}
			else if (index == IndexerEnum.Category)
			{
				Products = _khaiContext.Products.Include(x => x.Category).Include(x => x.ProductType).Include(x => x.Images).Where(x => x.CategoryId == id).ToList();
			}
			else if (index == IndexerEnum.Color)
			{
				 Products = _khaiContext.Products.Include(x => x.Category).Include(x => x.ProductType).Include(x => x.Images).Where(x => x.ColorId == id).ToList();
			}
			else if (index == IndexerEnum.Material)
			{
				Products = _khaiContext.Products.Include(x => x.Category).Include(x => x.ProductType).Include(x => x.Images).Where(x => x.MaterialId == id).ToList();
			}
			FilterShopViewModel filterShopView = new FilterShopViewModel
			{
				FilterProducts= Products,
				Colors = _khaiContext.Colors.ToList(),
				Categories = _khaiContext.Categories.ToList(),
				Materials = _khaiContext.Materials.ToList(),
				Types = _khaiContext.Types.ToList(),
				indexer= index,
			};


			return View(filterShopView);
		}

	}
}
