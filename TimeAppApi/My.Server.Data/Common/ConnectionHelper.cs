using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Server.Data.Common
{
    public class ConnectionHelper
    {
        private static string GetConnectionString(string nameOrConnectionString)
        {
            // try getting a settings first
            var settingValue = String.Empty;

            // check if we running in azure, since the code below cause EF6 to stop disposing of objects
            //if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("RoleRoot")))
            //{
            //    try
            //    {
            //        settingValue = CloudConfigurationManager.GetSetting(nameOrConnectionString); 连接存放在第三方云或者服务器
            //    }
            //    catch
            //    {

            //    }
            //}

            if (String.IsNullOrEmpty(settingValue))
            {
                var connectionStringVal = ConfigurationManager.ConnectionStrings[nameOrConnectionString];

                if (connectionStringVal == null) // we haven't found any setting, so it must be a connection string
                {
                    settingValue = nameOrConnectionString;
                }
                else
                {
                    settingValue = connectionStringVal.ConnectionString;
                }
            }

            return settingValue;
        }

        /// <summary>
        /// 获取数据库方言
        /// </summary>
        /// <returns></returns>
        private static string GetDefaultConnection(string nameOrConnectionString)
        {
            // OrmLiteConfig.DialectProvider = SqlServerDialect.Provider;
            OrmLiteConfig.DialectProvider = SqlServerOrmLiteDialectProvider.Instance;
            return GetConnectionString(nameOrConnectionString);
        }


        /// <summary>
        /// 获得连接对象
        /// </summary>
        /// <returns></returns>
        public static IDbConnection OpenDbConnection(string nameOrConnectionString)
        {
            try
            {
                return GetDefaultConnection(nameOrConnectionString).OpenDbConnection();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
