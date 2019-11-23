using System.Threading.Tasks;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
