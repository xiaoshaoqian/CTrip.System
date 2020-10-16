//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
//     author xiaoshaoqian
// </auto-generated>
//------------------------------------------------------------------------------

using CTrip.System.Common.Helpers;
using CTrip.System.Model;
using System.Diagnostics;
using System.Linq;
using SqlSugar;
using System;

namespace CTrip.System.Core
{
        /// <summary>
        /// 数据库上下文
        /// </summary>
    public class DbContext
    {

        public SqlSugarClient Db;   //用来处理事务多表查询和复杂的操作

        public static DbContext Current
        {
            get
            {
                return new DbContext();
            }
        }

        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = AppSettings.Configuration["DbConnection:ConnectionString"],
                DbType = (DbType)Convert.ToInt32(AppSettings.Configuration["DbConnection:DbType"]),
                IsAutoCloseConnection = true,
                IsShardSameThread = true,
                InitKeyType = InitKeyType.Attribute,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    DataInfoCacheService = new RedisCache()
                },
                MoreSettings = new ConnMoreSettings()
                {
                    IsAutoRemoveDataCache = true
                }
            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Debug.WriteLine(sql);
            };
        }

        public DbSet<T> DbTable<T>() where T : class, new()
        {
            return new DbSet<T>(Db);
        }

    }

    /// <summary>
    /// 扩展ORM
    /// </summary>
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context)
        {

        }
    }

}
