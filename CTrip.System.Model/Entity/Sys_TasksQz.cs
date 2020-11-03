﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
//     author xiaoshaoqian
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace CTrip.System.Model
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Sys_TasksQz")]
    public class Sys_TasksQz
    {
          public Sys_TasksQz()
          {
          }

           /// <summary>
           /// 描述 : 任务分组 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "任务分组")]           
           public string JobGroup {get;set;}

           /// <summary>
           /// 描述 : 触发器类型（0、simple 1、cron） 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "触发器类型（0、simple 1、cron）")]           
           public int TriggerType {get;set;}

           /// <summary>
           /// 描述 : 更新人编码 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "更新人编码")]           
           public string UpdateID {get;set;}

           /// <summary>
           /// 描述 : 运行时间表达式 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "运行时间表达式")]           
           public string Cron {get;set;}

           /// <summary>
           /// 描述 : 执行间隔时间(单位:秒) 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "执行间隔时间(单位:秒)")]           
           public int IntervalSecond {get;set;}

           /// <summary>
           /// 描述 : 更新人 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "更新人")]           
           public string UpdateName {get;set;}

           /// <summary>
           /// 描述 : 程序集名称 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "程序集名称")]           
           public string AssemblyName {get;set;}

           /// <summary>
           /// 描述 : 是否启动 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "是否启动")]           
           public bool IsStart {get;set;}

           /// <summary>
           /// 描述 : 任务所在类 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "任务所在类")]           
           public string ClassName {get;set;}

           /// <summary>
           /// 描述 : 传入参数 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "传入参数")]           
           public string JobParams {get;set;}

           /// <summary>
           /// 描述 : 任务描述 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "任务描述")]           
           public string Remark {get;set;}

           /// <summary>
           /// 描述 : 创建时间 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "创建时间")]           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// 描述 : 执行次数 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "执行次数")]           
           public int RunTimes {get;set;}

           /// <summary>
           /// 描述 : 最后更新时间 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "最后更新时间")]           
           public DateTime? UpdateTime {get;set;}

           /// <summary>
           /// 描述 : UID 
           /// 空值 : False
           /// 默认 : 
           /// <summary>
           [Display(Name = "UID")]           
           [SugarColumn(IsPrimaryKey=true)]
           public string ID {get;set;}

           /// <summary>
           /// 描述 : 开始时间 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "开始时间")]           
           public DateTime? BeginTime {get;set;}

           /// <summary>
           /// 描述 : 创建人编码 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "创建人编码")]           
           public string CreateID {get;set;}

           /// <summary>
           /// 描述 : 任务名称 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "任务名称")]           
           public string Name {get;set;}

           /// <summary>
           /// 描述 : 结束时间 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "结束时间")]           
           public DateTime? EndTime {get;set;}

           /// <summary>
           /// 描述 : 创建人 
           /// 空值 : True
           /// 默认 : 
           /// <summary>
           [Display(Name = "创建人")]           
           public string CreateName {get;set;}

    }
}
