using AbpWpfWithCM.Application;
using AbpWpfWithCM.Application.Contracts;
using AbpWpfWithCM.Domain;
using AbpWpfWithCM.EntityFramework;
using Caliburn.Micro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace AbpWpfWithCM.WpfApp
{
    [DependsOn(typeof(AbpAutofacModule),
        typeof(AbpEventBusModule),
        typeof(AbpWpfWithCMDomainMobule),
        typeof(AbpWpfWithCMApplicationContractsModule),
        typeof(AbpWpfWithCMEntityFrameworkModule),
        typeof(AbpWpfWithCMApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class AbpWpfWithCMModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IWindowManager, WindowManager>();
            context.Services.AddTransient<ShellViewModel>();
            context.Services.AddDbContext<AbpWpfWithCMDbContext>(options =>
            {
                options.UseSqlite();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite<AbpWpfWithCMDbContext>();
                options.UseSqlite<SettingManagementDbContext>();
            });
        }
    }
}
