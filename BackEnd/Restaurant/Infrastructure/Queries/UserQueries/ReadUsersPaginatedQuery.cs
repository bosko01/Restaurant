using Application.Queries.UserQueries;
using Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Application.Queries.UserQueries.ReadUsersPaginated;

namespace Infrastructure.Queries.User
{
    public class ReadUsersPaginatedQuery : IReadUsersPaginatedQuery
    {
        private RestaurantDbContext _dbContext;

        public ReadUsersPaginatedQuery(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Execute(Request request, CancellationToken cancellationToken = default)
        {
            var paginationParams = new Pagination(request.PagesToSkip, request.ItemsPerPage);
            var response = await _dbContext.Users
                .Skip(paginationParams.SkipNumber)
                .Take(paginationParams.ItemsPerPage)
                .ToListAsync();

            var responseAdapted = response.Adapt<List<ReadUsersPaginated.UserResponse>>();

            return new Response(responseAdapted);
        }
    }
}