using System;
using System.Threading.Tasks;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
        Task<User> GetByEmail(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
