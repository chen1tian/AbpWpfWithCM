using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using Volo.Abp.Validation;

namespace AbpWpfWithCM.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpValidationModule),
        typeof(AbpSettingsModule),
        typeof(AbpSettingManagementDomainModule)
    )]
    public class AbpWpfWithCMDomainMobule : AbpModule
    {

    }
}
