using AbpWpfWithCM.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Settings;

namespace AbpWpfWithCM.EntityFramework
{
    [DependsOn(
        typeof(AbpWpfWithCMDomainMobule),
        typeof(AbpMultiTenancyModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule)
        )]
    public class AbpWpfWithCMEntityFrameworkModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AbpWpfWithCMDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });            
        }
    }
}
