using OrderAPI.Models;

namespace OrderAPI.Interface
{
    public interface IOrder
    {
        MessageJ GetAllCustomer();
        MessageJ GetAllProduct();
        MessageJ GetProduct(string name);
        MessageJ GetAllOrder();
        MessageJ CreateOrderHeader(OrderHeaderModel obj);
        MessageJ CreateOrderItem(List<OrderItemModel> obj);
    }
}
