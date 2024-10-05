using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpWpfWithCM
{
    [DependsOn(typeof(AbpAutofacModule))]
    public class WpfAppModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IWindowManager, WindowManager>();
            context.Services.AddTransient<ShellViewModel>();
        }
    }
}
