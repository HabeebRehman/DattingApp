using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Helper
{
    public class LogUserActivty : IAsyncActionFilter
    {
        async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
           var resultContext=await next();
           if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
           var userid=resultContext.HttpContext.User.GetUserID();
           var repo=resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
           var user=await repo.GetUserByIDAsync(userid);
           user.LastActive=DateTime.Now;
           await repo.SaveAllAsync();
           
        }
    }
}