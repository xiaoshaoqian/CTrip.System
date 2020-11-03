using CTrip.System.Model;
using CTrip.System.Model.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CTrip.System.Hostd.Global
{
    public class GlobalActionMonitor:Attribute,IActionFilter
    {
        public GlobalActionMonitor()
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            #region 模型验证
            if (context.ModelState.IsValid) return;

            ApiResult response = new ApiResult();
            response.StatusCode = (int)StatusCodeType.ParameterError;

            foreach (var item in context.ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    if (!string.IsNullOrEmpty(response.Message))
                    {
                        response.Message += " | ";
                    }

                    response.Message += error.ErrorMessage;
                }
            }

            context.Result = new JsonResult(response);
            #endregion
        }
    }
}
