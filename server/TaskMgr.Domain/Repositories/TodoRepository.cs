using Framework.Mongo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Repositories
{
    public class TodoRepository : ITodoRepository
    {

        private readonly IMongoRepository<TodoItem, string> _repository;

        public TodoRepository(IMongoRepository<TodoItem, string> repository)
        {
            _repository = repository;
        }



        public async Task<IEnumerable<TodoItem>> All(DateTime fromDate, DateTime toDate, Entities.TaskStatus state)
        => await _repository.Find(item => item.DueDate >= fromDate.Date && item.DueDate <= toDate.Date && (int)item.State <= (int)state);

        public async Task Add(TodoItem item)
        => await _repository.Add(item);

        public async Task Delete(string id)
        => await _repository.Delete(id);

        public async Task Update(TodoItem item)
        => await _repository.Update(item);

        public async Task<TodoItem> Get(string Id)
        => await _repository.Get(Id);
    }
}
