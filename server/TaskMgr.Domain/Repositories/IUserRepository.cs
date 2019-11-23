using System;
using System.Threading.Tasks;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
