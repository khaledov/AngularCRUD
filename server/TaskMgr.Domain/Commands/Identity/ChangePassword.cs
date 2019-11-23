using Framework.CQRS.Commands;
using System;


namespace TaskMgr.Domain.Commands.Identity
{
    public class ChangePassword : ICommand
    {
        public Guid UserId { get; }
        public string CurrentPassword { get; }
        public string NewPassword { get; }

        public ChangePassword(Guid userId,
            string currentPassword, string newPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}
