using Framework.Authentication;
using Framework.CQRS.Dispatchers;
using Framework.MVC;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskMgr.Domain.Commands.Identity;
using TaskMgr.Domain.Queries;

namespace TaskMgr.Api.Controllers
{
    [Route("api/v1/auth")]
    [Produces("application/json")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class IdentityController : BaseController
    {
        public IdentityController(IDispatcher dispatcher) : base(dispatcher)
        {
        }
        [HttpGet("me")]
        [JwtAuth]
        public IActionResult Get() => Content($"Your id: '{UserId}'.");
               

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody]SignUp command)
        {
            command.Id = Guid.NewGuid().ToString();
            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody]SignIn query)
            => Ok(await _dispatcher.QueryAsync<JsonWebToken>(query));

        [HttpPut("me/password")]
        [JwtAuth]
        public async Task<ActionResult> ChangePassword([FromBody]ChangePassword command)
        {
            command.UserId = UserId;
           
            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpPut("random-pass")]
        public async Task<ActionResult> ChangeRandomPassword([FromBody]ChangePasswordWithRandom command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpPost("recover")]
        public async Task<IActionResult> RecoverPassword([FromBody]RecoverPassword command)
        {
            await _dispatcher.SendAsync(command);

            return NoContent();
        }
    }
}
