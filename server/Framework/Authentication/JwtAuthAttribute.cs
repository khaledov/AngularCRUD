using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Framework.Authentication
{
    public class JwtAuthAttribute : AuthorizeAttribute
    {
        public JwtAuthAttribute(string policy = "") :
            base(policy)
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
