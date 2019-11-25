using Framework;
using Framework.CQRS.Commands;
using Framework.EventBus.Abstractions;
using System;
using System.Threading.Tasks;
using TaskMgr.Domain.Commands.Tasks;
using TaskMgr.Domain.Events;
using TaskMgr.Domain.Repositories;

namespace TaskMgr.Domain.CommandHandlers
{
    public class TaskCommandsHandler :
        ICommandHandler<AddTask>,
        ICommandHandler<DeleteTask>,
        ICommandHandler<ChangeState>
    {
        private readonly ITodoRepository _todoRepository;
      
        private readonly IEventBus _eventBus;

        public TaskCommandsHandler(ITodoRepository todoRepository,IEventBus eventBus)
        {
            _eventBus = eventBus;
            _todoRepository = todoRepository;
        }

        public async Task HandleAsync(ChangeState command)
        {
           var task= await _todoRepository.Get(command.TaskId);
            if (task == null)
                throw new TaskMgrException(Codes.NotFound);
            task.State = command.NewState;
            await _todoRepository.Update(task);
        }

        public async Task HandleAsync(AddTask command)
        {
            var id = Guid.NewGuid().ToString();
            await _todoRepository.Add(new Entities.TodoItem
            {
                Id = id,
                Description = command.Description,
                Priority = command.Priority,
                DueDate = command.DueDate
            });

            await _eventBus.Publish<TaskCreatedEvent>(new TaskCreatedEvent(id,
                                                                            command.Description,
                                                                            command.DueDate,
                                                                            command.Priority));
        }
        

        public async Task HandleAsync(DeleteTask command)
        => await _todoRepository.Delete(command.TaskId);
    }
}
