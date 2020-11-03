using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CTrip.System.Model.Dto.System
{
    public class LoginDto
    {
        [Required(ErrorMessage = "登录账号不能为空")]
        [Display(Name = "登录账号")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "登录密码不能为空")]
        [Display(Name = "登录密码")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "验证码不能为空")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "验证码长度不正确")]
        [Display(Name = "验证码")]
        public string Code { get; set; }

        [Required(ErrorMessage = "验证码UID不能为空")]
        [Display(Name = "验证码UID")]
        public string Uuid { get; set; }
    }

    public class LoginMiniProgramDto
    {
        [Required(ErrorMessage = "登录账号不能为空")]
        [Display(Name = "登录账号")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "登录密码不能为空")]
        [Display(Name = "登录密码")]
        public string PassWord { get; set; }
    }
}
