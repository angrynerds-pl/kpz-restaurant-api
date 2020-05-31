using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var foundOrder = await _orderService.GetOrderById(orderId, restaurantId);

            if (foundOrder != null)
                return Ok(foundOrder);
            else
                return NotFound(foundOrder);
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

        [HttpGet("inProgress")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersInProgress()
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var orders = await _orderService.GetOrdersInProgress(restaurantId);
            if (orders != null)
                return Ok(orders);
            else
                return BadRequest(orders);

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

        [HttpGet("products/served/{orderId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderedProducts>>> GetServedProducts(int orderId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var servedProducts = await _orderService.GetServedProducts(orderId, restaurantId);

            if (servedProducts != null)
                return Ok(servedProducts);
            else
                return NotFound(servedProducts);
        }

        [HttpGet("table/{tableId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersForTable(int tableId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var ordersForTable = await _orderService.GetOrdersForTable(tableId, restaurantId);

            if (ordersForTable != null)
                return Ok(ordersForTable);
            else
                return NotFound(ordersForTable);

        }

        [HttpGet("history/{year}/{month}/{day}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByOrderDate(int year, int month, int day)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var orders = await _orderService.GetOrdersByOrderDate(year, month, day, restaurantId);

            if (orders != null)
                return Ok(orders);
            else
                return NotFound(orders);

        }

        [HttpGet("history/range")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersInOrderDateRange([FromBody] DateRange dateRange)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var orders = await _orderService.GetOrdersByOrderDateRange(dateRange, restaurantId);

            if (orders.Count() > 0)
                return Ok(orders);
            else
                return NotFound(orders);

        }

        [HttpGet("history/lastMonth")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersFromLastMonth()
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var orders = await _orderService.GetOrdersFromLastMonth(restaurantId);

            if (orders.Count() > 0)
                return Ok(orders);
            else
                return NotFound(orders);

        }


        [HttpGet("history/lastWeek")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersFromLastWeek()
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var orders = await _orderService.GetOrdersFromLastWeek(restaurantId);

            if (orders.Count() > 0)
                return Ok(orders);
            else
                return NotFound(orders);

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
                return BadRequest(products);
        }

        [HttpPut("products/{orderedProductId}")]
        [Authorize]
        public async Task<ActionResult<OrderedProducts>> UpdateOrderedProduct([FromBody] OrderedProducts orderedProduct, int orderedProductId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            orderedProduct.Id = orderedProductId;
            var products = await _orderService.UpdateOrderedProduct(orderedProduct, restaurantId);

            if (products != null)
                return Ok(products);
            else
                return BadRequest(products);
        }

        [HttpPut("{orderId}")]
        [Authorize]
        public async Task<ActionResult<OrderedProducts>> UpdateOrder([FromBody] Order order, int orderId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            order.Id = orderId;
            var updatedOrder = await _orderService.UpdateOrder(order, restaurantId);

            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return BadRequest(updatedOrder);
        }

        [HttpPut("{orderId}/{status}")]
        [Authorize]
        public async Task<ActionResult<OrderedProducts>> UpdateOrderStatus(int orderId, string status )
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User) && !_securityService.CheckIfInRole("COOK", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var updatedOrder = await _orderService.UpdateOrderStatus(orderId, status.ToUpper(), restaurantId);

            if (updatedOrder != null)
                return Ok(updatedOrder);
            else
                return BadRequest(updatedOrder);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateNewOrder([FromBody] Order order)
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

        [HttpPut("products")]
        [Authorize]
        public async Task<ActionResult<OrderedProducts>> UpdateManyOrderedProducts([FromBody] List<OrderedProducts> orderedProducts)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var updatedOrderedProducts = await _orderService.UpdateManyOrderedProducts(orderedProducts, restaurantId);

            if (updatedOrderedProducts.Count() > 0)
                return Ok(updatedOrderedProducts);
            else
                return BadRequest(updatedOrderedProducts);
        }

        [HttpDelete("products/{orderedProductId}")]
        [Authorize]
        public async Task<ActionResult<OrderedProducts>> DeleteOrderedProduct(int orderedProductId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var deletedOrderedProduct = await _orderService.DeleteOrderedProduct(orderedProductId, restaurantId);

            if (deletedOrderedProduct != null)
                return Ok(deletedOrderedProduct);
            else
                return NotFound(deletedOrderedProduct);
        }

    }
}