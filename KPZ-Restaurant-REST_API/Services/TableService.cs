using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class TableService : ITableService
    {
        private ITablesRepository _tablesRepository;

        public TableService(ITablesRepository tablesRepository)
        {
            _tablesRepository = tablesRepository;
        }

        public async Task<Table> AddNewTable(Table table, int restaurantId)
        {
            var tableCorrect = _tablesRepository.TableCorrect(table, restaurantId);
            var tableNotPresent = _tablesRepository.CheckIfTablePresent(table, restaurantId);

            if (!tableNotPresent.Result && tableCorrect.Result && table.Number > 0 && table.Seats > 0 && table.X >= 0 && table.Y >= 0)
            {
                await _tablesRepository.Add(table);
                await _tablesRepository.SaveAsync();
                return table;
            }
            else
                return null;

        }

        public async Task<IEnumerable<Table>> GetAllTablesByRoomId(int roomId, int restaurantId)
        {
            return await _tablesRepository.GetTablesByRoomId(roomId, restaurantId);
        }

        public async Task<Table> GetTableById(int id, int restaurantId)
        {
            return await _tablesRepository.GetTableById(id, restaurantId);
        }

        public async Task<IEnumerable<Table>> GetTablesFilterd(int restaurantId)
        {
            return await _tablesRepository.GetAllTablesFilteredBySeatsCount(restaurantId);
        }

        public async Task<Table> RemoveTableById(int id, int restaurantId)
        {
            var removedTable = await _tablesRepository.DeleteTableById(id, restaurantId);
            if (removedTable != null)
            {
                await _tablesRepository.SaveAsync();
                return removedTable;
            }
            else
                return null;
        }

        public async Task<Table> UpdateTable(int id, Table table, int restaurantId)
        {
            var entity = await _tablesRepository.GetTableById(id, restaurantId);

            if (entity != null)
            {
                entity.Number = table.Number;
                entity.RoomId = table.RoomId;
                entity.Seats = table.Seats;
                entity.Status = table.Status;
                entity.X = table.X;
                entity.Y = table.Y;

                _tablesRepository.Update(entity);
                await _tablesRepository.SaveAsync();
                return entity;
            }
            else
                return null;

        }

    }
}