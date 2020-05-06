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

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        private bool CheckIfInRole(string requiredRole)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role != requiredRole)
                return false;
            else
                return true;
        }

        [HttpGet("{tableId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Table>>> GetTableById(int tableId)
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var table = await _tableService.GetTableById(tableId);

            if (table != null)
                return Ok(table);
            else
                return NotFound(table);
        }

        [HttpGet("room/{roomId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Table>>> GetAllTablesByRoomId(int roomId)
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var tables = await _tableService.GetAllTablesByRoomId(roomId);

            if (tables != null)
                return Ok(tables);
            else
                return NotFound(tables);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Table>> AddTable([FromBody] Table table)
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var addedTable = await _tableService.AddNewTable(table);

            if (addedTable != null)
                return Ok(addedTable);
            else
                return Conflict(table);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Table>> ChangeTableAllocation(int id, [FromBody] Table table)
        {
            if (!CheckIfInRole("HEAD_WAITER") && !CheckIfInRole("WAITER") && !CheckIfInRole("MANAGER"))
                return Unauthorized();

            var updatedTable = await _tableService.UpdateTable(id, table);

            if (updatedTable != null)
                return Ok(updatedTable);
            else
                return NotFound(updatedTable);
        }

    }
}