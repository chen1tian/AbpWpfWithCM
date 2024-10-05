using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbpWpfWithCM.EntityFramework
{
    public static class EfMigrateMiddleWare
    {
        /// <summary>
        /// 自动合并数据库
        /// </summary>
        /// <param name="app"></param>
        /// <param name="db"></param>
        public static void UseAutoMigration(this IApplicationBuilder app, DbContext db)
        {
            MigrationDb(db);
        }

        /// <summary>
        /// 自动合并数据库
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="app"></param>
        public static void UseAutoMigration<TDbContext>(this IApplicationBuilder app)
            where TDbContext : DbContext
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();
                app.UseAutoMigration(dbContext);
            }
        }

        public static void MigrationDb(DbContext db)
        {
            // 自动迁移数据库 
            if (db.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine($"{db.GetType().Name} Migrating...");

                db.Database.Migrate();

                Console.WriteLine($"{db.GetType().Name} Database Migrated");
            }
            else
            {
                Console.WriteLine($"{db.GetType().Name} No Need Migrate");
            }

            Console.WriteLine($"{db.GetType().Name} Check Migrations Complete!");
            Console.WriteLine("");
        }
    }
}
