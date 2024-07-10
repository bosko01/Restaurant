using Domain.Interfaces.IRestaurant;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.RestaurantRepository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Restaurant> CreateNewAsync(Restaurant restaurant)
        {
            await _dbContext.AddAsync(restaurant);

            return restaurant;
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            _dbContext.Restaurants.Remove(restaurant);
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            var users = await _dbContext.Restaurants.ToListAsync<Restaurant>();

            return users;
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.Restaurants.FirstOrDefaultAsync(re => re.Id == id);

            return user;
        }

        public async Task<Restaurant> UpdateAsync(Restaurant restaurant)
        {
            _dbContext.Update(restaurant);
            return restaurant;
        }
    }
}