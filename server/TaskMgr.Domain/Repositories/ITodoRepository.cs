using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> All(DateTime fromDate, DateTime toDate, Entities.TaskStatus state);
        Task Add(TodoItem item);
        Task Update(TodoItem item);
        Task Delete(int id);
    }
}
