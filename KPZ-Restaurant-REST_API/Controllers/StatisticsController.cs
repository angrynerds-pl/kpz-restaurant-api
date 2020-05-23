using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private IStatisticsService _statisticsService;
        private ISecurityService _securityService;

        public StatisticsController(IStatisticsService statisticsService, ISecurityService securityService)
        {
            _statisticsService = statisticsService;
            _securityService = securityService;
        }

        [HttpGet("income")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<IncomeByMonth>>> GetIncomeFromPast6Months()
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetIncomeFromPast6Months(restaurantId));
        }

        [HttpGet("best")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductStatistics>>> GetTop5SellingProducts()
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetTop5SellingProducts(restaurantId));
        }

        [HttpGet("worst")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductStatistics>>> GetWorst5SellingProducts()
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetWorst5SellingProducts(restaurantId));
        }

        [HttpGet("categories")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductStatistics>>> GetAmountOfSoldProductsByCategory()
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetAmountOfSoldProductsByCategory(restaurantId));
        }


        [HttpGet("customers/today/{startTime}/{endTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CustomerTraffic>>> GetTodaysCustomerTraffic(int startTime, int endTime)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetCustomerTraffic(restaurantId, "TODAY", startTime, endTime));
        }

        [HttpGet("customers/week/{startTime}/{endTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CustomerTraffic>>> GetWeeksCustomerTraffic(int startTime, int endTime)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetCustomerTraffic(restaurantId, "WEEK", startTime, endTime));
        }

        [HttpGet("customers/month/{startTime}/{endTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CustomerTraffic>>> GetMonthsCustomerTraffic(int startTime, int endTime)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetCustomerTraffic(restaurantId, "MONTH", startTime, endTime));
        }

    }
}