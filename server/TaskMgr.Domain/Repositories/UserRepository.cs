using Framework.Mongo;
using System;
using System.Threading.Tasks;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User, Guid> _repository;

        public UserRepository(IMongoRepository<User, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<User> GetAsync(Guid id)
            => await _repository.Get(id);

        public async Task<User> GetAsync(string email)
            => await _repository.Get(x => x.Email == email.ToLowerInvariant());

        public async Task AddAsync(User user)
            => await _repository.Add(user);

        public async Task UpdateAsync(User user)
            => await _repository.Update(user);
    }
}
