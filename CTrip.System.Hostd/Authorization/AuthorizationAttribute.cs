﻿using CTrip.System.Common.Helpers;
using CTrip.System.Hostd.Extensions;
using CTrip.System.Model;
using CTrip.System.Model.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
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
            #region 判断是否登录
            var _tokenManager = context.HttpContext.RequestServices.GetService<TokenManager>();
            if (!_tokenManager.IsAuthenticated())
            {
                ApiResult response = new ApiResult
                {
                    StatusCode = (int)StatusCodeType.Unauthorized,
                    Message = StatusCodeType.Unauthorized.GetEnumText()
                };
                context.Result = new JsonResult(response)
                {
                    StatusCode = (int)StatusCodeType.Success
                };
                return;
            }
            #endregion

            #region 判断是否拥有权限
            if (!string.IsNullOrEmpty(Power))
            {
                if (Convert.ToBoolean(AppSettings.Configuration["AppSettings:Demo"]))
                {
                    ApiResult response = new ApiResult
                    {
                        StatusCode = (int)StatusCodeType.Error,
                        Message = "当前为演示模式 , 您无权修改任何数据"
                    };
                    context.Result = new JsonResult(response) { StatusCode = (int)StatusCodeType.Success };
                    return;
                }
                if (!_tokenManager.GetSessionInfo().UserPower.Contains(Power))
                {
                    ApiResult response = new ApiResult
                    {
                        StatusCode = (int)StatusCodeType.Forbidden,
                        Message = StatusCodeType.Forbidden.GetEnumText()
                    };

                    context.Result = new JsonResult(response) { StatusCode = (int)StatusCodeType.Success };
                    return;
                }
            }
            #endregion

        }

        
    }
}
