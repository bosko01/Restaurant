using Common.Exceptions;
using Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Application.Queries.Table.ReadAllRestaurantTables;

namespace Infrastructure.Queries.Table
{
    public class ReadAllRestaurantTablesQuery : IReadAllRestaurantTablesQuery
    {
        private RestaurantDbContext _dbContext;

        public ReadAllRestaurantTablesQuery(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Execute(Request request, CancellationToken cancellationToken = default)
        {
            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(restaurant => restaurant.Id == request.RestaurantId);

            if (restaurant is null)
            {
                throw new EntityNotFoundException("Restaurant not found");
            }

            var pagination = new Pagination(request.PagesToSkip, request.ItemsPerPage);
            var tables = await _dbContext.Tables.Where(t => t.RestaurantId == restaurant.Id).Skip(pagination.SkipNumber).Take(pagination.ItemsPerPage).ToListAsync();

            return new Response
            {
                Tables = tables.Adapt<List<TableResponse>>()
            };
        }
    }
}