using AbpWpfWithCM.Domain;
using System;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace AbpWpfWithCM.Application.Contracts
{
    [DependsOn(
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpWpfWithCMDomainMobule),
        typeof(AbpSettingManagementApplicationContractsModule)
        )]
    public class AbpWpfWithCMApplicationContractsModule : AbpModule
    {
    }
}
