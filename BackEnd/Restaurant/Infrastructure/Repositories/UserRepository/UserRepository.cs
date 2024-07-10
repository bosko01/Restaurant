using Domain.Interfaces.IUser;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public UserRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> CreateNewAsync(User user)
        {
            await _dbContext.AddAsync(user);

            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _dbContext.Users.ToListAsync<User>();

            return users;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
        }

        public async Task<User> UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            return user;
        }
    }
}