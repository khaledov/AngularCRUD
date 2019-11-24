using Framework.CQRS.Events;
using System.Threading.Tasks;
using TaskMgr.Api.Services;
using TaskMgr.Domain.Events;

namespace TaskMgr.Api.EventHandlers
{
    public class IdentityEventsHandler : IEventHandler<NewCodeGenerated>
    {
        readonly IEmailSender _emailSender;
        public IdentityEventsHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task HandleAsync(NewCodeGenerated @event)
        {

            await _emailSender.SendEmailAsync(@event.Email, @event.RandomPassword);
        }
    }
}
