//using E_commerce.Abstractions.MailService;
using E_commerce.Models;
using E_commerce.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace E_commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            builder.Services.AddDbContext<KhaiContext>(opt =>
            {
                opt.UseSqlServer("Server=JOJO;Database=Khai;Trusted_Connection=True");
            });

			builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequiredLength = 8;
				opt.Password.RequireLowercase = true;
				opt.Password.RequireUppercase = true;
				opt.Password.RequiredUniqueChars = 0;
				opt.Password.RequireDigit = true;


				opt.User.RequireUniqueEmail = false;

			}).AddEntityFrameworkStores<KhaiContext>().AddDefaultTokenProviders();
            //builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
			builder.Services.AddScoped<LayoutService>();
            //builder.Services.AddTransient<IMailService, MailService>();
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}