using CTrip.System.Hostd.Authorization;
using CTrip.System.Hostd.Extensions;
using CTrip.System.Interfaces;
using CTrip.System.Model;
using CTrip.System.Model.Dto.System;
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
    /// 日志接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogsController : BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<LogsController> _logger;

        /// <summary>
        /// 日志接口
        /// </summary>
        private readonly ISysLogService _logsService;

        public LogsController(ILogger<LogsController> logger, TokenManager tokenManager, ISysLogService logsService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _logsService = logsService;
        }


        /// <summary>
        /// 查询日志列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorization(Power = "PRIV_LOGS_VIEW")]
        public IActionResult Query([FromBody] LogsQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Sys_Log>();

            predicate = predicate.And(m => m.CreateTime >= parm.BeginDate && m.CreateTime < parm.EndDate.AddDays(1));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Level), m => m.Level == parm.Level);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.QueryText), m => m.Message.Contains(parm.QueryText) || m.Url.Contains(parm.QueryText) || m.IPAddress.Contains(parm.QueryText));

            var response = _logsService.GetPages(predicate.ToExpression(), parm);

            return ToResponse(response);
        }


    }
}
