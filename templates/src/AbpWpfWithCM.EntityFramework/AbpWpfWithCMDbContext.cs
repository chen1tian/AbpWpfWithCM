using AbpWpfWithCM.Domain.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace AbpWpfWithCM.EntityFramework
{
    [ConnectionStringName("Default")]
    public class AbpWpfWithCMDbContext : AbpDbContext<AbpWpfWithCMDbContext>
    {
        public AbpWpfWithCMDbContext(DbContextOptions<AbpWpfWithCMDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #region 实体
        /// <summary>
        /// App设置
        /// </summary>
        public virtual DbSet<Setting> AbpSettings { get; set; }

        /// <summary>
        /// App设置定义
        /// </summary>
        public DbSet<SettingDefinitionRecord> AbpSettingDefinitions { get; set; }
        #endregion

        #region 实体
        /// <summary>
        /// 刷新Token
        /// </summary>
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<User> User { get; set; }
        #endregion
    }
}
