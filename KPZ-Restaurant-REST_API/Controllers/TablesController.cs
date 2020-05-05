using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Services;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetAllTables()
        {
            var tables = await _tableService.GetAllTables();

            if (tables != null && tables.Count > 0)
                return Ok(tables);
            else
                return NotFound(tables);
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<IEnumerable<Table>>> GetAllTablesByRoomId(int roomId)
        {
            var tables = await _tableService.GetAllTablesByRoomId(roomId);

            if (tables != null)
                return Ok(tables);
            else
                return NotFound(tables);
        }

        [HttpPost]
        public async Task<ActionResult<Table>> AddTable([FromBody] Table table)
        {
            var addedTable = await _tableService.AddNewTable(table);

            if (addedTable != null)
                return Ok(addedTable);
            else
                return Conflict(table);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Table>> ChangeTableAllocation(int id, [FromBody] Table table)
        {
            var updatedTable = await _tableService.UpdateTable(id, table);

            if (updatedTable != null)
                return Ok(updatedTable);
            else
                return NotFound(updatedTable);

        }

    }
}