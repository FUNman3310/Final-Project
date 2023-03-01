using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Models
{
    public class KhaiContext:IdentityDbContext
    {
        public KhaiContext(DbContextOptions<KhaiContext> options):base(options) { }
        
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<ProductType> Types { get; set; }

        public DbSet<Product> Products { get; set; }

        public  DbSet<ProductImage> productImages { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Material> Materials { get; set; } 

        public DbSet<AboutPage> aboutPage { get; set; }

        public DbSet<RefundPage> refundPage { get; set; }

        public DbSet<TermsPage> termsPage { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<MyAccount> myAccounts { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<OrderItem> orderItems { get; set; }
     }
}
