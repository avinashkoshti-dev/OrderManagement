using Microsoft.OpenApi.Writers;
using OrderAPI.Interface;
using OrderAPI.Models;

namespace OrderAPI.Repository
{
    public class OrderRepo : IOrder
    {
        private readonly OrderMgmtContext db;

        public OrderRepo(OrderMgmtContext db)
        {
            this.db = db;
        }

        public MessageJ CreateOrderHeader(OrderHeaderModel obj)
        {
            MessageJ res = new MessageJ();
            OrderHeader oh = new OrderHeader();
            oh.OrderDate = obj.OrderDate;
            oh.OrderNo = obj.OrderNo;
            oh.CustomerId = obj.CustomerId;
            oh.TotalAmount = obj.TotalAmount;
            db.OrderHeaders.Add(oh);
            db.SaveChanges();
            res.status = true;
            res.data = oh;
            return res;
        }

        public MessageJ CreateOrderItem(List<OrderItemModel> obj1)
        {
            MessageJ res = new MessageJ();
            try
            {
                foreach (OrderItemModel item in obj1)
                {
                    OrderItem obj = new OrderItem();
                    obj.Id = item.Id;
                    obj.OrderHeaderId = item.OrderHeaderId;
                    obj.ProductId = item.ProductId;
                    obj.Rate = item.Rate;
                    obj.Qty = item.Qty;
                    obj.Total = item.Total;
                    obj.LineTotal = item.LineTotal;
                    obj.Gst = item.Gst;                   

                    db.OrderItems.Add(obj);
                    db.SaveChanges();
                    res.status = true;
                    res.message = "Order Added Successfully.";
                }
            }
            catch (Exception ex)
            {
                res.status = true;
                res.message = ex.Message.ToString();
            }
            return res;
        }

        public MessageJ GetAllCustomer()
        {
            MessageJ res = new MessageJ();
            res.data = db.Customers.ToList();
            return res;
        }

        public MessageJ GetAllOrder()
        {
            MessageJ res = new MessageJ();
            res.data = db.OrderHeaders.Select(x => new OrderHeaderModel()
            {
                CustomerId = x.CustomerId,
                CustomerName = x.Customer.CustomerName,
                Id = x.Id,
                OrderDate = x.OrderDate,
                OrderNo = x.OrderNo,
                TotalAmount = x.TotalAmount
            }).ToList();
            return res;
        }

        public MessageJ GetAllProduct()
        {
            MessageJ res = new MessageJ();
            res.data = db.Products.ToList();
            return res;
        }

        public MessageJ GetProduct(string name)
        {
            MessageJ res = new MessageJ();
            res.data = db.Products.Where(x => x.ProductName.Contains(name)).ToList();
            return res;
        }
    }
}
