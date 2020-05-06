using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;

        private bool CheckIfInRole(string requiredRole)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role != requiredRole)
                return false;
            return true;
        }

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var restaurantId = User.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value;
            var orders = await _orderService.GetAllOrders(int.Parse(restaurantId));
            return Ok(orders);
        }

        [HttpGet("products/{orderId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderedProducts>>> GetOrderedProducts(int orderId)
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var orderedProducts = await _orderService.GetOrderedProducts(orderId);
            if (orderedProducts != null)
                return Ok(orderedProducts);
            else
                return NotFound(orderedProducts);
        }

        [HttpGet("recent")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetRecentOrders()
        {
            //TO DO
            //Implement recent orders
            return Ok(await _orderService.GetAllOrders(1));
        }

        [HttpPost("products")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderedProducts>>> AddProductsToOrder([FromBody] List<OrderedProducts> orderedProducts)
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var products = await _orderService.AddOrderedProducts(orderedProducts);

            if (products != null)
                return Ok(products);
            else
                return NotFound(products);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNewOrder([FromBody] Order order)
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var createdOrder = await _orderService.CreateNewOrder(order);

            if (createdOrder != null)
                return Ok(createdOrder);
            else
                return BadRequest(createdOrder);
        }

    }
}