using Framework.CQRS.Commands;
using System;


namespace TaskMgr.Domain.Commands.Identity
{
    public class ChangePassword : ICommand
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public ChangePassword(string userId,
            string currentPassword, string newPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}
