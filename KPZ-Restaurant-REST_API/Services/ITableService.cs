using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface ITableService
    {
        Task<Table> UpdateTable(int id, Table table);
        Task<IList<Table>> GetAllTables();
        Task<Table> AddNewTable(Table table);

    } 
}