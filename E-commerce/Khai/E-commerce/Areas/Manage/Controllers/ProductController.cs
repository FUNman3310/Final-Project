using E_commerce.Helper;
using E_commerce.Models;
using E_commerce.Areas.Manage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace E_commerce.Areas.Manage.Controllers
{
	[Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
	{
		private readonly KhaiContext _khaiContext;
		private readonly IWebHostEnvironment _env;

		public ProductController(KhaiContext khaiContext,IWebHostEnvironment env)
		{
			_khaiContext = khaiContext;
			_env = env;
		}
		public IActionResult Index(int page = 1)
		{
			var query = _khaiContext.Products.AsQueryable();

			//PaginatedList<Product> paginatedProduct = new PaginatedList<Product>(query.Skip((page-1)*5).Take(5).ToList(),query.Count(),page,5);

			PaginatedList<Product> paginatedProducts = PaginatedList<Product>.Create(query,5 ,page);

			return View(paginatedProducts);
		}

		public IActionResult Create()
		{
			ViewBag.Category = _khaiContext.Categories.ToList();
			ViewBag.Color = _khaiContext.Colors.ToList();
			ViewBag.Material = _khaiContext.Materials.ToList();
			ViewBag.Type = _khaiContext.Types.ToList();


			return View();
		}
		[HttpPost]
		public IActionResult Create(ProductCreateViewModel productCreateView)
		{
			ViewBag.Category = _khaiContext.Categories.ToList();
			ViewBag.Color = _khaiContext.Colors.ToList();
			ViewBag.Material = _khaiContext.Materials.ToList();
			ViewBag.Type = _khaiContext.Types.ToList();

			if (!ModelState.IsValid) return View();

			Product product = new Product
			{
				Code= productCreateView.Code,
				Name= productCreateView.Name,
				Description= productCreateView.Description,
				Discount= productCreateView.Discount,
				ShortDescription= productCreateView.ShortDescription,
				CostPrice= productCreateView.CostPrice,
				Count= productCreateView.Count,
				IsAvailable= productCreateView.IsAvailable,
				IsRecommend = productCreateView.IsRecommend,
				IsTranding = productCreateView.IsTranding,
		        CategoryId= productCreateView.CategoryId,
				ColorId= productCreateView.ColorId,
				MaterialId= productCreateView.MaterialId,
				ProductTypeId= productCreateView.ProductTypeId,
				SalePrice= productCreateView.SalePrice
			};


			if(productCreateView.ImageFiles.Count>4) return View();

			if (productCreateView.ImageFiles != null)
			{
				foreach (IFormFile imageFile in productCreateView.ImageFiles)
				{
					if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
					{
						ModelState.AddModelError("ImageFiles", "Only png and jpeg format!");
						return View();
					}
					

					ProductImage productImage = new ProductImage
					{
						Product= product,
						Image = FileManager.SaveFile(_env.WebRootPath, "uploads/product/", imageFile),
						IsPoster = false
					};

					_khaiContext.productImages.Add(productImage);
				}
			}

			if (productCreateView.PosterImageFile != null)
			{

				if (productCreateView.PosterImageFile.ContentType != "image/jpeg" && productCreateView.PosterImageFile.ContentType != "image/png")
				{
					ModelState.AddModelError("PosterImageFile", "Only png and jpeg format!");
					return View();
				}
				

				ProductImage productImage = new ProductImage
				{
					Product = product,
					Image = FileManager.SaveFile(_env.WebRootPath, "uploads/product/", productCreateView.PosterImageFile),
					IsPoster = true
				};

				_khaiContext.productImages.Add(productImage);
			}


			_khaiContext.Products.Add(product);
			_khaiContext.SaveChanges();


			return RedirectToAction("index");
		}
		public IActionResult Update(int id)
		{
			ViewBag.Category = _khaiContext.Categories.ToList();
			ViewBag.Color = _khaiContext.Colors.ToList();
			ViewBag.Material = _khaiContext.Materials.ToList();
			ViewBag.Type = _khaiContext.Types.ToList();

			Product product = _khaiContext.Products.Include(x=>x.Images).FirstOrDefault(x=>x.Id ==id);
			if(product == null) return View("Error");


			return View(product);
		}
		[HttpPost]
		public IActionResult Update(int id, ProductUpdateViewModel  updateViewModel)
		{
			
			ViewBag.Category = _khaiContext.Categories.ToList();
			ViewBag.Color = _khaiContext.Colors.ToList();
			ViewBag.Material = _khaiContext.Materials.ToList();
			ViewBag.Type = _khaiContext.Types.ToList();

			int Count = 0;

			if (updateViewModel.ImageFiles != null)
			{
				Count = updateViewModel.ImageFiles.Count();
			}

			if (_khaiContext.productImages.Where(x => x.ProductId == id && x.IsPoster == false).Count() + Count > 4)
			{
				updateViewModel.ImageFiles.RemoveAll(x=>x !=null);
				
			}

			Product existProduct = _khaiContext.Products.Include(x=>x.Images).FirstOrDefault(b=>b.Id == id);

			if (existProduct == null) return View("Error");

			if (!ModelState.IsValid) return View(existProduct);

			
			existProduct.Images.RemoveAll(x=>!updateViewModel.ProductImageIds.Contains(x.Id)&&x.IsPoster==false);


			if (updateViewModel.ImageFiles != null)
			{
				foreach (var imageFile in updateViewModel.ImageFiles)
				{
					if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
					{
						ModelState.AddModelError("ImageFiles", "Only png and jpeg format!");
						return View();
					}
					

					ProductImage productImage = new ProductImage
					{
						ProductId = id,
						Image = FileManager.SaveFile(_env.WebRootPath, "uploads/product/", imageFile),
						IsPoster = false
					};

					existProduct.Images.Add(productImage);
				}
			}

			if (updateViewModel.PosterImageFile != null)
			{
				string name = existProduct.Images.FirstOrDefault(x => x.IsPoster == true).Image;

				existProduct.Images.Remove(existProduct.Images.FirstOrDefault(x => x.IsPoster == true));

				FileManager.DeleteFile(_env.WebRootPath, "uploads/product/", name);


				if (updateViewModel.PosterImageFile.ContentType != "image/jpeg" && updateViewModel.PosterImageFile.ContentType != "image/png")
				{
					ModelState.AddModelError("PosterImageFile", "Only png and jpeg format!");
					return View();
				}


				ProductImage productImage = new ProductImage
				{
					ProductId = existProduct.Id,
					Image = FileManager.SaveFile(_env.WebRootPath, "uploads/product/", updateViewModel.PosterImageFile),
					IsPoster = true
				};

				existProduct.Images.Add(productImage);
			}

			existProduct.Name = updateViewModel.Name;
			existProduct.CategoryId = updateViewModel.CategoryId;
			existProduct.ProductTypeId = updateViewModel.ProductTypeId;
			existProduct.ColorId = updateViewModel.ColorId;
			existProduct.Count = updateViewModel.Count;
			existProduct.MaterialId = updateViewModel.MaterialId;
			existProduct.Code = updateViewModel.Code;
			existProduct.Description = updateViewModel.Description;
			existProduct.ShortDescription = updateViewModel.ShortDescription;
			existProduct.CostPrice = updateViewModel.CostPrice;
			existProduct.SalePrice = updateViewModel.SalePrice;
			existProduct.Discount = updateViewModel.Discount;
			existProduct.IsAvailable = updateViewModel.IsAvailable;
			existProduct.IsRecommend= updateViewModel.IsRecommend;
			existProduct.IsTranding= updateViewModel.IsTranding;


			_khaiContext.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Delete(int id)
		{
			Product product = _khaiContext.Products.FirstOrDefault(x => x.Id == id);

			if (product == null) return NotFound();
			//ProductImage posterImage = _khaiContext.productImages.FirstOrDefault(b => b.ProductId == product.Id);

			List<ProductImage> productImages = new List<ProductImage>();

			foreach (var item in _khaiContext.productImages.Where(x=>x.ProductId==product.Id))
			{
				productImages.Add(item);
			}

			foreach (ProductImage item in productImages)
			{
				FileManager.DeleteFile(_env.WebRootPath, "uploads/product/", item.Image);

				_khaiContext.productImages.Remove(item);
			}


			 _khaiContext.Products.Remove(product);

			_khaiContext.SaveChanges();
			return RedirectToAction("index");
		}
	}
}
