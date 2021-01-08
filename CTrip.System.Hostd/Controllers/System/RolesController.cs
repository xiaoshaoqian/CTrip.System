﻿using CTrip.System.Hostd.Authorization;
using CTrip.System.Hostd.Extensions;
using CTrip.System.Interfaces;
using CTrip.System.Model;
using CTrip.System.Model.Dto.System;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTrip.System.Hostd.Controllers.System
{
    /// <summary>
    /// 角色定义
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<RolesController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;


        /// <summary>
        /// 角色定义接口
        /// </summary>
        private readonly ISysRoleService _roleService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenManager"></param>
        /// <param name="roleService"></param>
        /// <param name="logger"></param>
        public RolesController(TokenManager tokenManager, ISysRoleService roleService, ILogger<RolesController> logger)
        {
            _tokenManager = tokenManager;
            _roleService = roleService;
            _logger = logger;
        }


        /// <summary>
        /// 查询角色定义列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] RolesQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Sys_Role>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Name), m => m.Name.Contains(parm.Name));

            var response = _roleService.GetPages(predicate.ToExpression(), parm);

            return ToResponse(response);
        }


        /// <summary>
        /// 查询角色定义
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return ToResponse(_roleService.GetId(id));
            }
            return ToResponse(_roleService.GetAll().OrderBy(m => m.CreateTime));
        }


        /// <summary>
        /// 添加角色定义
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_ROLES_CREATE")]
        public IActionResult Create([FromBody] RolesCreateDto parm)
        {
            if (_roleService.Any(m => m.Name == parm.Name))
            {
                return ToResponse(StatusCodeType.Error, $"添加 {parm.Name} 失败，该数据已存在，不能重复！");
            }

            //从 Dto 映射到 实体
            var role = parm.Adapt<Sys_Role>().ToCreate(_tokenManager.GetSessionInfo());

            return ToResponse(_roleService.Add(role));
        }

        /// <summary>
        /// 更新角色定义
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_ROLES_UPDATE")]
        public IActionResult Update([FromBody] RolesUpdateDto parm)
        {
            if (_roleService.Any(m => m.Name == parm.Name && m.ID != parm.ID))
            {
                return ToResponse(StatusCodeType.Error, $"更新 {parm.Name} 失败，该数据已存在，不能重复！");
            }

            var userSession = _tokenManager.GetSessionInfo();

            return ToResponse(_roleService.Update(m => m.ID == parm.ID, m => new Sys_Role()
            {
                Name = parm.Name,
                Remark = parm.Remark,
                UpdateID = userSession.UserID,
                UpdateName = userSession.UserName,
                UpdateTime = DateTime.Now
            }));
        }

        /// <summary>
        /// 删除角色定义
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_ROLES_DELETE")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return ToResponse(StatusCodeType.Error, "删除角色 Id 不能为空");
            }

            var response = _roleService.Delete(id);

            return ToResponse(response);
        }
    }
}
