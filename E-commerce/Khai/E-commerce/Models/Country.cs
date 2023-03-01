namespace E_commerce.Models
{
	public class Country
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<MyAccount> myAccounts { get; set; }

		public List<Order> orders { get; set; }
	}
}
