using Framework.CQRS.Commands;

namespace TaskMgr.Domain.Commands.Identity
{
    public class ChangePasswordWithRandom : ICommand
    {
        public string Email { get; }
        public string RandomPassword { get; }
        public string NewPassword { get; }

        public ChangePasswordWithRandom(string email,
            string randomPassword, string newPassword)
        {
            Email = email;
            RandomPassword = randomPassword;
            NewPassword = newPassword;
        }
    }
}
