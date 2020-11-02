using CTrip.System.Hostd.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTrip.System.Hostd.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public string Power { get; set; } = ""; //权限标识

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var _tokenManager = context.HttpContext.RequestServices.GetService<TokenManager>();
        }

    }
}
