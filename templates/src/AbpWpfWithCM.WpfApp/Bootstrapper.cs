using AbpWpfWithCM.EntityFramework;
using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp;

namespace AbpWpfWithCM.WpfApp
{
    public class Bootstrapper : BootstrapperBase
    {
        private IAbpApplicationWithInternalServiceProvider? _abpApplication;

        public Bootstrapper()
        {
            Initialize();
        }


        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File("Logs/logs.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true
                )
            .CreateLogger();

            try
            {
                Log.Information("Starting WPF host.");

                _abpApplication = await AbpApplicationFactory.CreateAsync<AbpWpfWithCMModule>(options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
                });

                await _abpApplication.InitializeAsync();

                //_abpApplication.Services.GetRequiredService<MainWindow>()?.Show();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }


            // 自动初始化数据库
            AutoMigration();

            await DisplayRootViewForAsync<ShellViewModel>();
        }

        /// <summary>
        /// 自动初始化数据库
        /// </summary>
        private void AutoMigration()
        {
            var dbContext = _abpApplication.ServiceProvider.GetRequiredService<AbpWpfWithCMDbContext>();
            EfMigrateMiddleWare.MigrationDb(dbContext);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _abpApplication.ServiceProvider.GetRequiredKeyedService(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _abpApplication.Services.Where(s => s.ServiceType == service).Select(s => s.ImplementationInstance);
        }

        protected override async void OnExit(object sender, EventArgs e)
        {
            if (_abpApplication != null)
            {
                await _abpApplication.ShutdownAsync();
            }
            Log.CloseAndFlush();

            base.OnExit(sender, e);
        }
    }
}
