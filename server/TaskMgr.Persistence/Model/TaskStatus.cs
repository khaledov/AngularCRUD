namespace TaskMgr.Persistence.Model
{
    public enum TaskStatus
    {
        Pending = 0,
        InProgress = 1,
        Completed = 2,
        Standby = 4,
        All = Pending | InProgress | Completed | Standby
    }
}
