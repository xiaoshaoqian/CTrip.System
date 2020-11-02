using CTrip.System.Model;
using CTrip.System.Model.Api;
using CTrip.System.Model.View.System;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTrip.System.Hostd.Controllers
{
    public class BaseController : ControllerBase
    {
        #region 返回封装
        public static JsonResult ToResponse(StatusCodeType statusCode)
        {
            ApiResult response = new ApiResult()
            {
                StatusCode = (int)statusCode,
                Message = statusCode.GetEnumText()
            };
            return new JsonResult(response);
        }
        public static JsonResult ToResponse(StatusCodeType statusCode, string retMessage)
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
            ApiResult<T> response = new ApiResult<T>()
            {
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
        /// <summary>
        /// 生成系统菜单树
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<UserMenusVM> ResolveUserMenuTree(List<Sys_Menu> menus, string parentId = null)
        {
            List<UserMenusVM> userMenus = new List<UserMenusVM>();

            foreach (var menu in menus.Where(m => m.ParentUID == parentId).OrderBy(m => m.SortIndex))
            {
                var childrenMenu = ResolveUserMenuTree(menus, menu.ID);
                UserMenusVM menusVM = new UserMenusVM
                {
                    name = menu.Name,
                    path = parentId == null ? "/" + menu.Path : menu.Path,
                    hidden = menu.Hidden,
                    component = menu.Component ?? "layout",
                    redirect = parentId == null ? "noredirect" : null,
                    meta = new MenuMetaVM() { title = menu.Name, icon = menu.Icon, path = menu.Path, keepAlive = menu.KeepAlive },
                    children = childrenMenu.Count == 0 ? null : childrenMenu
                };
                if (childrenMenu.Count == 0 && menu.Component == null)
                {
                    continue;
                }
                userMenus.Add(menusVM);
            }
            return userMenus;
        }

        /// <summary>
        /// 生成  Router
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<MenuListVM> ResolveMenuTree(List<Sys_Menu> menus, string parentId = null)
        {
            List<MenuListVM> resultMenus = new List<MenuListVM>();

            foreach (var menu in menus.Where(m => m.ParentUID == parentId).OrderBy(m => m.SortIndex))
            {
                var childrenMenu = ResolveMenuTree(menus, menu.ID);

                MenuListVM menusVM = new MenuListVM
                {
                    ID = menu.ID,
                    Name = menu.Name,
                    Icon = menu.Icon,
                    Path = menu.Path,
                    Component = menu.Component,
                    SortIndex = menu.SortIndex,
                    ViewPower = menu.ViewPower,
                    ParentUID = menu.ParentUID,
                    Remark = menu.Remark,
                    Hidden = menu.Hidden,
                    System = menu.System,
                    isFrame = menu.isFrame,
                    KeepAlive = menu.KeepAlive,
                    Children = childrenMenu.Count == 0 ? null : childrenMenu,
                    CreateTime = menu.CreateTime,
                    UpdateTime = menu.UpdateTime,
                    CreateID = menu.CreateID,
                    CreateName = menu.CreateName,
                    UpdateID = menu.UpdateID,
                    UpdateName = menu.UpdateName

                };
                resultMenus.Add(menusVM);
            }

            return resultMenus;
        }
        #endregion
    }
}
