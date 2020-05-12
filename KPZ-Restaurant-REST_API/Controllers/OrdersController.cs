using System.Collections.Generic;
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
        private ISecurityService _securityService;

        public OrdersController(IOrderService orderService, ISecurityService securityService)
        {
            _orderService = orderService;
            _securityService = securityService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var orders = await _orderService.GetAllOrders(restaurantId);
            return Ok(orders);
        }

        [HttpGet("products/{orderId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderedProducts>>> GetOrderedProducts(int orderId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var orderedProducts = await _orderService.GetOrderedProducts(orderId, restaurantId);

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
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var products = await _orderService.AddOrderedProducts(orderedProducts, restaurantId);

            if (products != null)
                return Ok(products);
            else
                return NotFound(products);
        }

        [HttpPut("products")]
        [Authorize]
        public async Task<ActionResult<OrderedProducts>> UpdateOrderedProduct([FromBody] OrderedProducts orderedProduct)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var products = await _orderService.UpdateOrderedProduct(orderedProduct, restaurantId);

            if (products != null)
                return Ok(products);
            else
                return NotFound(products);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<OrderedProducts>> UpdateOrder([FromBody] Order order)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var updatedOrder = await _orderService.UpdateOrder(order, restaurantId);

            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return NotFound(updatedOrder);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNewOrder([FromBody] Order order)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);

            var createdOrder = await _orderService.CreateNewOrder(order, restaurantId);

            if (createdOrder != null)
                return Ok(createdOrder);
            else
                return BadRequest(createdOrder);
        }

    }
}