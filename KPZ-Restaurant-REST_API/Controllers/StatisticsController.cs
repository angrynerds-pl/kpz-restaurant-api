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
        public async Task<ActionResult<IEnumerable<IncomeByMonth>>> GetIncomeFromPast6Months([FromQuery(Name="startDate")] string startDate, [FromQuery(Name="endDate")] string endDate)
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

        [HttpGet("best/range")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductStatistics>>> GetTop5SellingProducts([FromQuery(Name="startDate")] string startDate, [FromQuery(Name="endDate")] string endDate)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetTop5SellingProductsFromTimePeriod(startDate, endDate, restaurantId));
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

        [HttpGet("worst/range")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductStatistics>>> GetWorst5SellingProducts([FromQuery(Name="startDate")] string startDate, [FromQuery(Name="endDate")] string endDate)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetWorst5SellingProductsFromTimePeriod(startDate, endDate, restaurantId));
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


        [HttpGet("categories/range")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductStatistics>>> GetAmountOfSoldProductsByCategory([FromQuery(Name="startDate")] string startDate, [FromQuery(Name="endDate")] string endDate)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetAmountOfSoldProductsByCategoryFromTimePeriod(startDate, endDate, restaurantId));
        }

        [HttpGet("customers/today")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CustomerTraffic>>> GetTodaysCustomerTraffic([FromQuery(Name="startTime")] int startTime, [FromQuery(Name="endTime")]  int endTime)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetCustomerTraffic(restaurantId, "TODAY", startTime, endTime));
        }

        [HttpGet("customers/week")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CustomerTraffic>>> GetWeeksCustomerTraffic([FromQuery(Name="startTime")] int startTime, [FromQuery(Name="endTime")]  int endTime)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetCustomerTraffic(restaurantId, "WEEK", startTime, endTime));
        }

        [HttpGet("customers/month")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CustomerTraffic>>> GetMonthsCustomerTraffic([FromQuery(Name="startTime")] int startTime, [FromQuery(Name="endTime")]  int endTime)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetCustomerTraffic(restaurantId, "MONTH", startTime, endTime));
        }

    }
}