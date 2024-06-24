using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Interface;
using OrderAPI.Models;
using OrderAPI.Repository;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly OrderMgmtContext db;
        private readonly OrderRepo _orderRepo;

        public APIController(OrderMgmtContext db)
        {
            this.db = db;
            _orderRepo = new OrderRepo(db);
        }

        [HttpGet("GetAllCustomer")]
        public MessageJ GetAllCustomer()
        {
            return _orderRepo.GetAllCustomer();
        }

        [HttpGet("GetAllProduct")]
        public MessageJ GetAllProduct()
        {
            return _orderRepo.GetAllProduct();
        }

        [HttpGet("GetProduct/{name}")]
        public MessageJ GetProduct(string name)
        {
            return _orderRepo.GetProduct(name);
        }

        [HttpGet("GetAllOrder")]
        public MessageJ GetAllOrder()
        {
            return _orderRepo.GetAllOrder();
        }

        [HttpPost("CreateOrderHeader")]
        public MessageJ CreateOrderHeader(OrderHeaderModel obj)
        {
            return _orderRepo.CreateOrderHeader(obj);
        }

        [HttpPost("CreateOrderItem")]
        public MessageJ CreateOrderItem(List<OrderItemModel> obj)
        {
            return _orderRepo.CreateOrderItem(obj);
        }
    }
}
