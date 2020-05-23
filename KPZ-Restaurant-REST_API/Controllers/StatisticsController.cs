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
        public async Task<ActionResult<IEnumerable<SelledProduct>>> GetTop5SellingProducts()
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();   

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetTop5SellingProducts(restaurantId));
        }

        [HttpGet("worst")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SelledProduct>>> GetWorst5SellingProducts()
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();   

            var restaurantId = _securityService.GetRestaurantId(User);
            return Ok(await _statisticsService.GetWorst5SellingProducts(restaurantId));
        }


    }
}