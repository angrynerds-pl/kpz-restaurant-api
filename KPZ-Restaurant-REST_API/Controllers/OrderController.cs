using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("addNew")]
        public IActionResult CreateNewOrder([FromBody] Order order)
        {
            var createdOrder = _orderService.CreateNewOrder(order);

            if (createdOrder != null)
            {
                return Ok(createdOrder);
            }
            else
            {
                return BadRequest(null);
            }
        }


    }
}