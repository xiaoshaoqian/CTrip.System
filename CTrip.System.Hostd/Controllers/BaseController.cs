using CTrip.System.Model;
using CTrip.System.Model.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTrip.System.Hostd.Controllers
{
    public class BaseController:ControllerBase
    {
        #region 返回封装
        public static JsonResult ToResponse(StatusCodeType statusCode)
        {
            ApiResult response = new ApiResult() {
                StatusCode = (int)statusCode,
                Message = statusCode.GetEnumText()
            };
            return new JsonResult(response);
        }
        public static JsonResult ToResponse(StatusCodeType statusCode,string retMessage)
        {
            ApiResult response = new ApiResult()
            {
                StatusCode = (int)statusCode,
                Message = retMessage
            };
            return new JsonResult(response);
        }
        public static JsonResult ToResponse<T>(T data)
        {
            ApiResult<T> response = new ApiResult<T>() { 
                StatusCode = (int)StatusCodeType.Success,
                Message = StatusCodeType.Success.GetEnumText(),
                Data = data
            };
            return new JsonResult(response);
        }
        #endregion

        #region 常用方法函数
        public static string GetGUID
        {
            get { return Guid.NewGuid().ToString().ToUpper(); }
        }
        #endregion

        #region 生成菜单树
        //public static List<UserMenusVM> ResolveUserMenuTree(List<Sys_Menu> menus,string parenId = null)
        //{
            
        //}
        #endregion
    }
}
