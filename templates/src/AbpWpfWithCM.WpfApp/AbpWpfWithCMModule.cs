using AbpWpfWithCM.Application;
using AbpWpfWithCM.Application.Contracts;
using AbpWpfWithCM.Domain;
using AbpWpfWithCM.EntityFramework;
using AutoMapper;
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
            // 依赖注入
            context.Services.AddSingleton<IWindowManager, WindowManager>();
            context.Services.AddSingleton<IEventAggregator, EventAggregator>();

            context.Services.AddTransient<ShellViewModel>();


            // 数据库依赖注入
            context.Services.AddDbContext<AbpWpfWithCMDbContext>(options =>
            {
                options.UseSqlite();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite<AbpWpfWithCMDbContext>();
                options.UseSqlite<SettingManagementDbContext>();
            });

            // 配置AutoMapper
            ConfigureAutoMap(context);
        }

        /// <summary>
        /// 配置AutoMapper
        /// </summary>
        /// <param name="context"></param>
        public void ConfigureAutoMap(ServiceConfigurationContext context)
        {
            // 对象映射
            context.Services.AddAutoMapperObjectMapper<AbpWpfWithCMApplicationModule>();
            // 对象映射
            context.Services.AddAutoMapperObjectMapper<AbpWpfWithCMModule>();

            var services = context.Services;
            AutoMapper.IConfigurationProvider config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AbpWpfWithCMApplicationAutoMapProfile>();
                cfg.AddProfile<AbpWpfWithCMWpfAppAutoMapProfile>();
            });
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AbpWpfWithCMApplicationModule>(validate: true);
                options.AddMaps<AbpWpfWithCMModule>(validate: true);
            });
        }
    }
}
