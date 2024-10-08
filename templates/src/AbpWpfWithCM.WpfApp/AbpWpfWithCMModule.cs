using AbpWpfWithCM.EntityFramework;
using Caliburn.Micro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace AbpWpfWithCM.WpfApp
{
    [DependsOn(typeof(AbpAutofacModule))]
    public class AbpWpfWithCMModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IWindowManager, WindowManager>();
            context.Services.AddTransient<ShellViewModel>();

            Configure<AbpDbContextOptions>(options =>
            {
                context.Services.AddDbContext<AbpWpfWithCMDbContext>(options =>
                {
                    options.UseSqlite();
                });
                options.UseSqlite<AbpWpfWithCMDbContext>();
                options.UseSqlite<SettingManagementDbContext>();
            });
        }
    }
}
