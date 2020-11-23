using CTrip.System.Common.Helpers;
using CTrip.System.Common.Utilities;
using CTrip.System.Core;
using CTrip.System.Hostd.Authorization;
using CTrip.System.Hostd.Extensions;
using CTrip.System.Interfaces;
using CTrip.System.Model;
using CTrip.System.Model.Dto.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTrip.System.Hostd.Controllers.System
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController:BaseController
    {
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly ISysUsersService _userService;

        /// <summary>
        /// 用户关系接口
        /// </summary>
        private readonly ISysUserRelationService _userRelationService;

        public AuthController(TokenManager tokenManager, ISysUsersService userService, ILogger<AuthController> logger, ISysUserRelationService userRelationService)
        {
            _tokenManager = tokenManager;
            _userService = userService;
            _logger = logger;
            _userRelationService = userRelationService;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Code()
        {
            var code = CaptchaUtil.GetRandomEnDigitalText();

            var verifyCode = CaptchaUtil.GenerateCaptchaImage(code);

            RedisServer.Cache.Set($"Captcha:{verifyCode.CaptchaGUID}", verifyCode.CaptchaCode, 1800);

            JObject result = new JObject();

            result.Add("captchaCode", $"data:image/png;base64,{Convert.ToBase64String(verifyCode.CaptchaMemoryStream.ToArray())}");
            result.Add("captchaGUID", verifyCode.CaptchaGUID);

            return ToResponse(result);
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginDto parm)
        {
            var captchaCode = RedisServer.Cache.Get($"Captcha:{parm.Uuid}");

            RedisServer.Cache.Del($"Captcha:{parm.Uuid}");

            if (parm.Code.ToUpper() != captchaCode)
            {
                return ToResponse(StatusCodeType.Error, "输入验证码无效");
            }
            var userInfo = _userService.GetFirst(o => o.UserName == parm.UserName.Trim() || o.Phone == parm.UserName.Trim());
            if (userInfo == null)
            {
                return ToResponse(StatusCodeType.Error, "用户名或密码错误");
            }

            if (!PasswordUtil.ComparePasswords(userInfo.UserID, userInfo.Password, parm.PassWord.Trim()))
            {
                return ToResponse(StatusCodeType.Error, "用户名或密码错误");
            }

            if (!userInfo.Enabled)
            {
                return ToResponse(StatusCodeType.Error, "用户未启用，请联系管理员！");
            }

            var userToken = _tokenManager.CreateSession(userInfo, SourceType.Web, Convert.ToInt32(AppSettings.Configuration["AppSettings:WebSessionExpire"]));

            return ToResponse(userToken);
        }

        /// <summary>
        /// 微信小程序用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LoginMiniProgram([FromBody] LoginMiniProgramDto parm)
        {
            var userInfo = _userService.GetFirst(o => o.UserID == parm.UserName.Trim());

            if (userInfo == null)
            {
                return ToResponse(StatusCodeType.Error, "用户名或密码错误");
            }

            if (!PasswordUtil.ComparePasswords(userInfo.UserID, userInfo.Password, parm.PassWord.Trim()))
            {
                return ToResponse(StatusCodeType.Error, "用户名或密码错误");
            }

            if (!userInfo.Enabled)
            {
                return ToResponse(StatusCodeType.Error, "用户未启用，请联系管理员！");
            }

            var userToken = _tokenManager.CreateSession(userInfo, SourceType.MiniProgram, Convert.ToInt32(AppSettings.Configuration["AppSettings:MiniProgramSessionExpire"]));

            return ToResponse(userToken);
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LogOut()
        {
            _tokenManager.RemoveSession(_tokenManager.GetSysToken);

            return ToResponse(StatusCodeType.Success);
        }

        /// <summary>
        /// 用户信息获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public IActionResult GetUserInfo()
        {
            return ToResponse(_tokenManager.GetSessionInfo());
        }
        [HttpGet]
        public IActionResult GetDemo()
        {
            return ToResponse(new Sys_Role()
            {
                CreateID = "xiaoshaoqian",
                CreateName = "肖绍谦",
                CreateTime = DateTime.Now
            });
        }
    }
}
