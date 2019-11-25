using Framework.Authentication;
using Framework.CQRS.Queries;

namespace TaskMgr.Domain.Queries
{
    public class SignIn : IQuery<JsonWebToken>
    {
        public string Email { get; }
        public string Password { get; }

        public SignIn(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
