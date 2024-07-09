using Domain.Interfaces.IUserCredentials;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.UserCredentialsRepository
{
    public class UserCredentialsRepository  : IUserCredentialsRepository
    {
        private RestaurantDbContext _dbContext;

        public UserCredentialsRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserCredentials> CreateNewAsync(UserCredentials t)
        {
            var user = await _dbContext.AddAsync(t);

            return t;
        }

        public async Task DeleteAsync(UserCredentials userCredentials)
        {
            _dbContext.UserCredentials.Remove(userCredentials);
        }

        public async Task<List<UserCredentials>> GetAllAsync()
        {
            var userCredentials = await _dbContext.UserCredentials.ToListAsync<UserCredentials>();

            return userCredentials;
        }

        public async Task<UserCredentials?> GetByIdAsync(Guid id)
        {
            var userCredentials = await _dbContext.UserCredentials.FirstOrDefaultAsync(x => x.Id == id);

            return userCredentials;
        }

        public async Task<UserCredentials> UpdateAsync(UserCredentials t)
        {
           _dbContext.UserCredentials.Update(t);

            return t;
        }
    }
}