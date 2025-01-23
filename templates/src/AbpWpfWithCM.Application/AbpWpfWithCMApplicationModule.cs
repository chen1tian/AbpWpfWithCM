using AbpWpfWithCM.Application.Contracts;
using AbpWpfWithCM.Domain;
using AbpWpfWithCM.EntityFramework;
using AutoMapper;
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
            
        }
    }
}
