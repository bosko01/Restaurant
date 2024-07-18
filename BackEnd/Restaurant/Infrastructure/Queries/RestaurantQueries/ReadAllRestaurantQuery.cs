using Application.Queries.Restaurant;
using Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Application.Queries.Restaurant.ReadAllRestaurants;

namespace Infrastructure.Queries.RestaurantQueries
{
    internal class ReadAllRestaurantQuery : IReadAllRestaurantsQuery
    {
        private RestaurantDbContext _dbContext;

        public ReadAllRestaurantQuery(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Execute(Request request, CancellationToken cancellationToken = default)
        {
            var pagination = new Pagination(request.PagesToSkip, request.ItemsPerPage);

            var restaurants = await _dbContext.Restaurants.Skip(pagination.SkipNumber).Take(pagination.ItemsPerPage).ToListAsync();

            return new Response(restaurants.Adapt<List<ReadAllRestaurants.RestaurantResponse>>());
        }
    }
}