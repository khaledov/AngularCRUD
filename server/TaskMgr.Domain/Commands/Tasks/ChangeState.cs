using Framework.CQRS.Commands;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Domain.Commands.Tasks
{
   public class ChangeState : ICommand
    {
        public ChangeState(string taskId, TaskStatus newState)
        {
            TaskId = taskId;
            NewState = newState;
        }

        public string TaskId { get; set; }
        public TaskStatus NewState { get; set; }

        

        
    }
}
