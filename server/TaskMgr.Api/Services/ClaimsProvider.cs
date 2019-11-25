using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskMgr.Domain.Services;

namespace TaskMgr.Api.Services
{
    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetAsync(string userId)
        {
            //add your custom claims here
            return await Task.FromResult(new Dictionary<string, string>());
        }
    }
}
