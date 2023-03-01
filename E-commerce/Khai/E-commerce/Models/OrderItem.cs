namespace E_commerce.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public double SalePrice { get; set; }

		public double CostPrice { get; set; }

		public int Discount { get; set; }

		public int Count { get; set; }
		public Order order { get; set; }
		public Product Product { get; set; }
	}
}
