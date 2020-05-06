using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        private bool CheckIfInRole(string requiredRole)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role != requiredRole)
                return false;
            else
                return true;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>> CreateNewProduct([FromBody] Product product)
        {
            if (!CheckIfInRole("MANAGER"))
                return Unauthorized();

            var createdProduct = await _menuService.CreateNewProduct(product);

            if (createdProduct != null)
                return Ok(createdProduct);
            else
                return BadRequest(createdProduct);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
             if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

             var restaurantId = User.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value;
             return await _menuService.GetAllProducts(int.Parse(restaurantId));
        }

        [HttpPost("categories")]
        [Authorize]
        public async Task<ActionResult<Category>> CreateNewCategory([FromBody] Category category)
        {
            if (!CheckIfInRole("MANAGER"))
                return Unauthorized();

            var createdCategory = await _menuService.CreateNewCategory(category);

            if (createdCategory != null)
                return Ok(createdCategory);
            else
                return BadRequest(createdCategory);
        }

        [HttpGet("categories")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories([FromBody] Category category)
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var restaurantId = User.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value;
            var categories = await _menuService.GetAllCategories(int.Parse(restaurantId));
            return Ok(categories);
        }



    }
}