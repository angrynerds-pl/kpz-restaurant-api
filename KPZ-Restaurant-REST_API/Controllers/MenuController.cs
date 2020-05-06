using System;
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

        [HttpPost("{categoryName}")]
        [Authorize]
        public async Task<ActionResult<Product>> CreateNewProduct([FromBody] Product product, [FromRoute] string categoryName)
        {
            if (!CheckIfInRole("MANAGER"))
                return Unauthorized();

            var restaurantId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value);
            var createdProduct = await _menuService.CreateNewProduct(product, categoryName, restaurantId);

            if (createdProduct != null)
                return Ok(createdProduct);
            else
                return NotFound(createdProduct);
        }

        [HttpGet("{categoryName}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryName([FromRoute] string categoryName)
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var restaurantId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value);
            var products = await _menuService.GetProductsByCategoryName(restaurantId, categoryName);
            if (products != null)
                return Ok(products);
            else
                return NotFound(products);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var restaurantId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value);
            var products = await _menuService.GetAllProducts(restaurantId);
            return Ok(products);
        }


        [HttpPost("categories")]
        [Authorize]
        public async Task<ActionResult<Category>> CreateNewCategory([FromBody] Category category)
        {
            if (!CheckIfInRole("MANAGER"))
                return Unauthorized();

            var restaurantId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value);
            var createdCategory = await _menuService.CreateNewCategory(category, restaurantId);

            if (createdCategory != null)
                return Ok(createdCategory);
            else
                return BadRequest(createdCategory);
        }

        [HttpGet("categories")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var restaurantId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value);
            var categories = await _menuService.GetAllCategories(restaurantId);
            return Ok(categories);
        }



    }
}