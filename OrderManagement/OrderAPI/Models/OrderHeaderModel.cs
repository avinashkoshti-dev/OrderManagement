namespace OrderAPI.Models
{
    public class OrderHeaderModel
    {
        public int Id { get; set; }

        public string OrderNo { get; set; } = null!;

        public string OrderDate { get; set; } = null!;

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;

        public double TotalAmount { get; set; }
    }
}
