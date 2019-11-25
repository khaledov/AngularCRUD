using Framework.Mongo;
using System;
using System.Threading.Tasks;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken, string> _repository;

        public RefreshTokenRepository(IMongoRepository<RefreshToken, string> repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _repository.Get(x => x.Token == token);

        public async Task AddAsync(RefreshToken token)
            => await _repository.Add(token);

        public async Task UpdateAsync(RefreshToken token)
            => await _repository.Update(token);
    }
}
