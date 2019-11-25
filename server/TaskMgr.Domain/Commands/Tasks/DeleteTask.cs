using Framework.CQRS.Commands;

namespace TaskMgr.Domain.Commands.Tasks
{
    public class DeleteTask:ICommand
    {
        public DeleteTask(string taskId)
        {
            TaskId = taskId;
        }

        public string TaskId { get; set; }
    }
}
