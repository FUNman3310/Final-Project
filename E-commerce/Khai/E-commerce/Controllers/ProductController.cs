using E_commerce.Models;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;


namespace E_commerce.Controllers
{
	public class ProductController : Controller
	{
		private readonly KhaiContext _khaiContext;
		private readonly UserManager<AppUser> _userManager;

		public ProductController(KhaiContext khaiContext,UserManager<AppUser> userManager)
		{
			_khaiContext = khaiContext;
			_userManager = userManager;
		}
		public IActionResult Detail(int id)
		{
			Product product = _khaiContext.Products.Include(x=>x.Color).Include(x=>x.Category).Include(x=>x.ProductType).Include(x=>x.Material).Include(x=>x.Images).FirstOrDefault(x=>x.Id== id);
			if (product == null) return View("Error");

			ProductDetailViewModel productDetail = new ProductDetailViewModel
			{
				product= product,
				RelatedProducts = _khaiContext.Products.Include(x => x.Category).Include(x=>x.ProductType).Include(x => x.Images).Where(x => x.CategoryId == product.CategoryId&& x.ProductTypeId==product.ProductTypeId && x.Id != product.Id).ToList(),
			};


			return View(productDetail);
		}


        public async Task<IActionResult> AddToBasket(int productId)
        {
			#region Cookie
			//List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

			//BasketItemViewModel basketItem = null;

			//string basketItemStr = HttpContext.Request.Cookies["Basket"];

			//if (basketItemStr!=null)
			//{
			//    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemStr);

			//    basketItem = basketItems.FirstOrDefault(x=>x.ProductId==productId);

			//    if (basketItem != null) basketItem.Count++;
			//    else
			//    {
			//        basketItem = new BasketItemViewModel
			//        {
			//             ProductId = productId,
			//             Count = 1
			//        };
			//        basketItems.Add(basketItem);
			//    }



			//}
			//else
			//{
			//   basketItem = new BasketItemViewModel
			//   {
			//       ProductId = productId,
			//       Count = 1
			//   };

			//   basketItems.Add(basketItem);

			//}

			//basketItemStr = JsonConvert.SerializeObject(basketItem);

			//HttpContext.Response.Cookies.Append("Basket", basketItemStr);
			#endregion
            if (!_khaiContext.Products.Any(x=>x.Id==productId)) return NotFound();

            AppUser member = null;


			if (User.Identity.IsAuthenticated)
			{
				member = await _userManager.FindByNameAsync(User.Identity.Name);
			}
			else
			{
				return RedirectToAction("login", "account");
			}

			if(member == null)
			{
				return NotFound();
			}
			else
			{
				BasketItem memberBasketItem = _khaiContext.BasketItems.FirstOrDefault(x=>x.AppUserId==member.Id&&x.ProductId==productId);
				if (memberBasketItem != null) memberBasketItem.Count++;
				else
				{
					memberBasketItem = new BasketItem
					{
						AppUserId = member.Id,
						ProductId = productId,
						Count = 1

					};
					_khaiContext.BasketItems.Add(memberBasketItem);
				}
			}
			_khaiContext.SaveChanges();

			return RedirectToAction("checkout","product");
        }

   //     public IActionResult GetBasket()
   //     {
			////List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

			////string basketItemStr = HttpContext.Request.Cookies["Basket"];

			////if (basketItemStr != null)
			////{
			////    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemStr);

			////}

			////return Json(basketItems);
			//return Ok();
   //     }

        public async Task<IActionResult> Checkout()
        {
			#region Cookie

			//List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

			//         CheckoutItemViewModel checkoutItem = null;

			//         string basketItemsStr = HttpContext.Request.Cookies["BasketItems"];

			//         if (basketItemsStr!=null)
			//         {
			//             basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

			//             foreach (var item in basketItems)
			//             {
			//                 checkoutItem = new CheckoutItemViewModel
			//                 {
			//                     product = _khaiContext.Products.FirstOrDefault(x=>x.Id==item.ProductId),
			//                     Count= item.Count,
			//                 };
			//                 checkoutItems.Add(checkoutItem);
			//             }
			//}
			#endregion
			ViewBag.Country = _khaiContext.Countries.ToList();
			List<CheckoutItemViewModel> productCheckoutItems = new List<CheckoutItemViewModel>();
			List<BasketItem> memberBasketItems = new List<BasketItem>();
			OrderViewModel orderView = null;
			CheckoutItemViewModel productCheckoutItem = null;
			AppUser member = null;

			if (User.Identity.IsAuthenticated)
			{
				member = await _userManager.FindByNameAsync(User.Identity.Name);
			}
			else
			{
				return RedirectToAction("login", "account");
			}

			if (member == null)
			{
				return NotFound();
			}
			else
			{
				memberBasketItems = _khaiContext.BasketItems.Include(x=>x.Product).Where(x=>x.AppUserId==member.Id).ToList();

				foreach (var item in memberBasketItems)
				{
					productCheckoutItem = new CheckoutItemViewModel
					{
						product = item.Product,
						Count = item.Count
					};

					productCheckoutItems.Add(productCheckoutItem);
				}
			}


			orderView = new OrderViewModel
			{
				checkoutItemViews= productCheckoutItems,
				Fullname =member.Fullname,
				Email= member.Email
			};


			return View(orderView);    
        }
		[HttpPost]
		public async Task<IActionResult> Order(OrderViewModel orderView)
		{
			if(!ModelState.IsValid) return RedirectToAction("checkout","product");
			ViewBag.Country = _khaiContext.Countries.ToList();
			List<CheckoutItemViewModel> productCheckoutItems = new List<CheckoutItemViewModel>();
			List<BasketItem> memberBasketItems = new List<BasketItem>();
			CheckoutItemViewModel productCheckoutItem = null;
			AppUser member = null;
			OrderItem orderItem = null;
			double totalPrice = 0;


			if (User.Identity.IsAuthenticated)
			{
				member = await _userManager.FindByNameAsync(User.Identity.Name);
			}
			else
			{
				return RedirectToAction("login", "account");
			}

			Order order = new Order
			{
				FullName = orderView.Fullname,
				Email= orderView.Email,
				Address= orderView.Address,
				CountryId= orderView.CountryId,
				Mobile = orderView.Mobile,
				Note= orderView.Note,
				CreateTime = DateTime.UtcNow.AddHours(4),
				AppUserId =member.Id
			};


			if (member == null)
			{
				return NotFound();
			}
			else
			{
				memberBasketItems = _khaiContext.BasketItems.Include(x => x.Product).Where(x => x.AppUserId == member.Id).ToList();
				if(memberBasketItems.Count == 0) return RedirectToAction("checkout", "product");
				foreach (var item in memberBasketItems)
				{
					Product product = _khaiContext.Products.FirstOrDefault(x => x.Id == item.ProductId);
					if (product == null) return NotFound();
					orderItem = new OrderItem
					{
						Product = product,
						ProductName = product.Name,
						CostPrice= product.CostPrice,
						Discount = product.Discount,
						Count = item.Count,
						SalePrice= (product.SalePrice*(1-(product.Discount/100)))
						
						
					};
					totalPrice += orderItem.SalePrice * orderItem.Count;
					order.OrderItems.Add(orderItem);
				}
				foreach (var item in memberBasketItems)
				{
					_khaiContext.BasketItems.Remove(item);
				}

				order.TotalCost = totalPrice;
				_khaiContext.orders.Add(order);
				_khaiContext.SaveChanges();
			}

			return RedirectToAction("index","home");
		}

		
    }
}
