using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("products/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderedProducts>>> GetOrderedProducts(int orderId)
        {
            var orderedProducts = await _orderService.GetOrderedProducts(orderId);
            if (orderedProducts != null)
                return Ok(orderedProducts);
            else
                return NotFound(orderedProducts);
        }

        [HttpPost("products")]
        public async Task<ActionResult<IEnumerable<OrderedProducts>>> AddProductsToOrder([FromBody] List<OrderedProducts> orderedProducts)
        {
            var products = await _orderService.AddOrderedProducts(orderedProducts);

            if (products != null)
                return Ok(products);
            else
                return NotFound(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewOrder([FromBody] Order order)
        {
            var createdOrder = await _orderService.CreateNewOrder(order);

            if (createdOrder != null)
                return Ok(createdOrder);
            else
                return BadRequest(createdOrder);
        }

    }
}