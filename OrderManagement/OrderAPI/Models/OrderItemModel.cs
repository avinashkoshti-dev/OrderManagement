namespace OrderAPI.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        public int OrderHeaderId { get; set; }

        public int ProductId { get; set; }

        public double Rate { get; set; }

        public int Qty { get; set; }

        public int Gst { get; set; }

        public double Total { get; set; }

        public double LineTotal { get; set; }
    }
}
