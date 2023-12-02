using PudgeSMomom.Models;

namespace PudgeSMomom.Services.Repository.UserRepository
{
    public interface IUserRepository
    {
        public Task<User> GetUserByIdAsync(int id);
        public Task<IEnumerable<User>> GetUsersAsync(int id);
        public Task<User> GetUserByNameAsync(string name);
    }
}
