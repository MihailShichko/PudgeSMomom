using Microsoft.EntityFrameworkCore;
using PudgeSMomom.Data;
using PudgeSMomom.Models;

namespace PudgeSMomom.Services.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext applicationDbContext) 
        {
            _dbContext = applicationDbContext;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id.ToString());
            return user;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.UserName == name);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int id)
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
