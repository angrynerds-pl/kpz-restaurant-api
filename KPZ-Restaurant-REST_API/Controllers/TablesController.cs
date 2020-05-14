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
    public class TablesController : ControllerBase
    {
        private ITableService _tableService;
        private ISecurityService _securityService;

        public TablesController(ITableService tableService, ISecurityService securityService)
        {
            _tableService = tableService;
            _securityService = securityService;
        }

        [HttpGet("{tableId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Table>>> GetTableById(int tableId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var table = await _tableService.GetTableById(tableId, restaurantId);

            if (table != null)
                return Ok(table);
            else
                return NotFound(table);
        }

        [HttpDelete("{tableId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Table>>> RemoveTableById(int tableId)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var table = await _tableService.RemoveTableById(tableId, restaurantId);

            if (table != null)
                return Ok(table);
            else
                return NotFound(table);
        }

        [HttpGet("room/{roomId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Table>>> GetAllTablesByRoomId(int roomId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var tables = await _tableService.GetAllTablesByRoomId(roomId, restaurantId);

            if (tables != null)
                return Ok(tables);
            else
                return NotFound(tables);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Table>> AddTable([FromBody] Table table)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User)
                && !_securityService.CheckIfInRole("WAITER", User)
                && !_securityService.CheckIfInRole("MANAGER", User)
                && table.Room.RestaurantId != _securityService.GetRestaurantId(User))
            {
                return Unauthorized();
            }

            var restaurantId = _securityService.GetRestaurantId(User);
            var addedTable = await _tableService.AddNewTable(table, restaurantId);

            if (addedTable != null)
                return Ok(addedTable);
            else
                return Conflict(table);
        }

        [HttpGet("filtered")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Table>>> GetTablesFiltered()
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User)
                && !_securityService.CheckIfInRole("WAITER", User)
                && !_securityService.CheckIfInRole("MANAGER", User))
            {
                return Unauthorized();
            }

            var restaurantId = _securityService.GetRestaurantId(User);
            var filteredTables = await _tableService.GetTablesFilterd(restaurantId);

            if (filteredTables != null)
                return Ok(filteredTables);
            else
                return BadRequest(filteredTables);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Table>> ChangeTableAllocation(int id, [FromBody] Table table)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User)
                && !_securityService.CheckIfInRole("WAITER", User)
                && !_securityService.CheckIfInRole("MANAGER", User))
            {
                return Unauthorized();
            }

            var restaurantId = _securityService.GetRestaurantId(User);
            var updatedTable = await _tableService.UpdateTable(id, table, restaurantId);

            if (updatedTable != null)
                return Ok(updatedTable);
            else
                return NotFound(updatedTable);
        }

    }
}