using Framework.CQRS.Commands;

namespace TaskMgr.Domain.Commands.Identity
{
    public class RecoverPassword : ICommand
    {
        public string Email { get; set; }

    }
}
