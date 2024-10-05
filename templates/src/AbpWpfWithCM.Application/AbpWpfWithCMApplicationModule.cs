using AbpWpfWithCM.Application.Contracts;
using AbpWpfWithCM.Domain;
using AbpWpfWithCM.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace AbpWpfWithCM.Application
{
    [DependsOn(
        typeof(AbpWpfWithCMDomainMobule),
        typeof(AbpWpfWithCMEntityFrameworkModule),
        typeof(AbpWpfWithCMApplicationContractsModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpSettingManagementApplicationModule)
        )]
    public class AbpWpfWithCMApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 对象映射
            context.Services.AddAutoMapperObjectMapper<AbpWpfWithCMApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AbpWpfWithCMApplicationModule>(validate: true);
            });
        }
    }
}
