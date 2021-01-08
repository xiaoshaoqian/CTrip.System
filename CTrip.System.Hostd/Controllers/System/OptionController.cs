using CTrip.System.Hostd.Authorization;
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
    /// 字典定义
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OptionsController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<OptionsController> _logger;

        /// <summary>
        /// 字典定义接口
        /// </summary>
        private readonly ISysOptionsService _optionService;

        public OptionsController(TokenManager tokenManager, ISysOptionsService optionService, ILogger<OptionsController> logger)
        {
            _tokenManager = tokenManager;
            _optionService = optionService;
            _logger = logger;
        }


        /// <summary>
        /// 查询字典定义列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult Query([FromBody] OptionsQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Sys_Options>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.Option.Contains(parm.QueryText));

            var response = _optionService.GetPages(predicate.ToExpression(), parm);

            return ToResponse(response);
        }


        /// <summary>
        /// 查询字典定义
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return ToResponse(_optionService.GetId(id));
            }
            return ToResponse(_optionService.GetAll());
        }

        /// <summary>
        /// 根据分组查询字典
        /// </summary>
        /// <param name="option">字典分组</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetOption(string option)
        {
            if (string.IsNullOrEmpty(option))
            {
                return ToResponse(StatusCodeType.Error, "group 不能为空");
            }

            var response = (_optionService.GetWhere(m => m.Option == option)).OrderBy(m => m.Option).OrderBy(m => m.SortIndex);

            return ToResponse(response);
        }

        /// <summary>
        /// 添加字典定义
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_OPTIONS_CREATE")]
        public IActionResult Create([FromBody] OptionsCreateDto parm)
        {
            //从 Dto 映射到 实体
            var options = parm.Adapt<Sys_Options>().ToCreate(_tokenManager.GetSessionInfo());

            return ToResponse(_optionService.Add(options));
        }

        /// <summary>
        /// 更新字典定义
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_OPTIONS_UPDATE")]
        public IActionResult Update([FromBody] OptionsUpdateDto parm)
        {
            var userSession = _tokenManager.GetSessionInfo();

            return ToResponse(_optionService.Update(m => m.ID == parm.ID, m => new Sys_Options()
            {
                Option = parm.Option,
                Label = parm.Label,
                Value = parm.Value,
                SortIndex = parm.SortIndex,
                Remark = parm.Remark,
                UpdateID = userSession.UserID,
                UpdateName = userSession.UserName,
                UpdateTime = DateTime.Now
            }));
        }

        /// <summary>
        /// 删除字典定义
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(Power = "PRIV_OPTIONS_DELETE")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return ToResponse(StatusCodeType.Error, "删除字典 Id 不能为空");
            }

            var response = _optionService.Delete(id);

            return ToResponse(response);
        }
    }
}
