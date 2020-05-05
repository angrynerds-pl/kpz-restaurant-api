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

        public async Task<Table> AddNewTable(Table table)
        {
            var tableInDatabase = await _tablesRepository.FindOne(t =>
                t.Number == table.Number &&
                t.RoomId == table.RoomId
            );

            if (tableInDatabase == null)
            {
                await _tablesRepository.Add(table);
                await _tablesRepository.SaveAsync();
                return table;
            } else
                return null;

        }

        public async Task<IList<Table>> GetAllTables()
        {
            return await _tablesRepository.GetAll();
        }

        public async Task<IEnumerable<Table>> GetAllTablesByRoomId(int roomId)
        {
            return await _tablesRepository.GetWhere(t => t.RoomId == roomId);
        }

        public async Task<Table> UpdateTable(int id, Table table)
        {
            var entity = await _tablesRepository.FindOne(t => t.Id == id);

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