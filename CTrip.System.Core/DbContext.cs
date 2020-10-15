using System;
using System.Configuration;
using CTrip.System.Common;
using CTrip.System.Common.Helpers;
using SqlSugar;

namespace CTrip.System.Core
{
    public class DbContext
    {
        public SqlSugarClient Db;

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
                }
            });


        }
    }
}
