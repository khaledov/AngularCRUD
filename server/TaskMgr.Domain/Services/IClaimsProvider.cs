using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskMgr.Domain.Services
{
    public interface IClaimsProvider
    {
        Task<IDictionary<string, string>> GetAsync(string userId);
    }
}
