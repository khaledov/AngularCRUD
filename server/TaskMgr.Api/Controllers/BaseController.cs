using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.CQRS.Dispatchers;
using Framework.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;

namespace TaskMgr.Api.Controllers
{
   
    public class BaseController : ControllerBase
    {
        protected readonly IDispatcher _dispatcher;

        public BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
      
        protected Guid UserId
        {
            get
            {
                Guid.TryParse(User.FindFirst("sub")?.Value, out Guid id);
                return id;
            }
        }


        protected async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
            => await _dispatcher.QueryAsync<TResult>(query);

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        protected ActionResult<IList<TEntity>> Collection<TEntity>(IList<TEntity> result)
        {
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}