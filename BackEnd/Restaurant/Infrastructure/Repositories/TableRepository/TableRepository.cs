using Domain.Interfaces.ITable;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.TableRepository
{
    public class TableRepository : ITableRepository
    {
        private RestaurantDbContext _dbContext;

        public TableRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Table> CreateNewAsync(Table table)
        {
            await _dbContext.AddAsync(table);

            return table;
        }

        public async Task DeleteAsync(Table table)
        {
            _dbContext.Tables.Remove(table);
        }

        public async Task<List<Table>> GetAllAsync()
        {
            return await _dbContext.Tables.ToListAsync<Table>();
        }

        public async Task<Table?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Tables.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Table> UpdateAsync(Table table)
        {
            _dbContext.Tables.Update(table);
            return table;
        }
    }
}