using E_commerce.Models;
using Microsoft.AspNetCore.Identity;

namespace E_commerce.Services
{
	public class LayoutService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LayoutService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<AppUser> GetUser()
		{
			AppUser appUser = null;

			if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
			{
				appUser = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
			}

			if (appUser != null) return appUser;

			return null;
		}
	}
}
