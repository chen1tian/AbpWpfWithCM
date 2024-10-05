using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Volo.Abp.EntityFrameworkCore;

namespace AbpWpfWithCM.EntityFramework.MySql
{
    /// <summary>
    /// 数据库扩展
    /// </summary>
    public static class MySqlExtensions
    {
        /// <summary>
        /// 使用SqlServer数据库
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionString"></param>
        public static void UseMySqlDb<TDbContext>([NotNull] this AbpDbContextOptions options,
                                                string? connectionString = null,
                                                bool useMigrationsAssembly = true)
            where TDbContext : AbpDbContext<TDbContext>
        {
            // 得到迁移程序集的名称
            var migrationsAssembly = typeof(MySqlExtensions).GetTypeInfo().Assembly.GetName().Name;
            // 建立一个委托，用于配置SqlServerDbContextOptionsBuilder
            Action<MySqlDbContextOptionsBuilder>? optionsAction = b => b.MigrationsAssembly(migrationsAssembly);
            // 如果不使用迁移程序集，则将委托置空
            if (useMigrationsAssembly == false)
            {
                optionsAction = null;
            }

            options.Configure<TDbContext>(ctx =>
            {
                // 如果上下文中的连接字符串与参数的连接字符串都为空，则抛出异常
                if (ctx.ConnectionString.IsNullOrWhiteSpace() && connectionString.IsNullOrWhiteSpace())
                {
                    throw new Exception("无法注册DbContext，连接字符串为空！");
                }

                //  优先使用参数中的连接字符串，其次使用上下文中的连接字符串
                if (connectionString.IsNullOrWhiteSpace())
                {
                    connectionString = ctx.ConnectionString;
                }

                // 如果上下文中存在连接，则使用上下文中的连接，否则使用参数中的连接
                if (ctx.ExistingConnection != null)
                {
                    var serverVersion = ServerVersion.Parse(ctx.ExistingConnection.ServerVersion);
                    ctx.DbContextOptions.UseMySql(ctx.ExistingConnection, serverVersion, optionsAction);
                }
                else
                {
                    var serverVersion = ServerVersion.AutoDetect(connectionString);
                    ctx.DbContextOptions.UseMySql(connectionString, serverVersion, optionsAction);
                }
            });
        }
    }
}
