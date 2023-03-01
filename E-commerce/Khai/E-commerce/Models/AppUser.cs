using Microsoft.AspNetCore.Identity;

namespace E_commerce.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }

        public bool IsAdmin { get; set; } 

        public List<BasketItem> basketItems { get; set; }

        public MyAccount myAccount { get; set; }
    }
}
