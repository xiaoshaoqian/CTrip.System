using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CTrip.System.Model.Dto.System
{
    public class UpdateUserRelationDto
    {
        [Required(ErrorMessage = "用户编码不能为空")]
        [Display(Name = "用户编码")]
        public string UserID { get; set; }

        [Display(Name = "用户权限")]
        public List<UserRelationDto> Relation { get; set; }
    }

    public class UserRelationDto
    {
        [Display(Name = "数据权限ID")]
        public string ObjectID { get; set; }

        [Display(Name = "数据权限类型")]
        public string ObjectType { get; set; }
    }
}
