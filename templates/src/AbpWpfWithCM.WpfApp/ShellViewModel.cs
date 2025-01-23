using AbpWpfWithCM.Domain.Localization.Resources;
using AbpWpfWithCM.WpfApp.Modules;
using AbpWpfWithCM.WpfApp.Systems.Modules;
using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.VirtualFileSystem;

namespace AbpWpfWithCM.WpfApp
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IStringLocalizer<AbpWpfWithCMResources> _localizer;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 字符串本地化处理器
        /// </summary>
        public IStringLocalizer<AbpWpfWithCMResources> L => _localizer;

        public ShellViewModel(IStringLocalizer<AbpWpfWithCMResources> localizer, IServiceProvider serviceProvider)
        {
            _localizer = localizer;

            _serviceProvider = serviceProvider;

            // 初始化模块
            InitModules(_serviceProvider);

            DisplayName = "App名称";
        }

        #region 模块
        /// <summary>
        /// 模块列表
        /// </summary>
        public ObservableCollection<IModule> Modules { get; set; } = new ObservableCollection<IModule>();

        /// <summary>
        /// 是
        /// </summary>
        /// <param name="serviceProvider"></param>
        private void InitModules(IServiceProvider serviceProvider)
        {
            AddModule<TestViewModel>(serviceProvider);
        }

        /// <summary>
        /// 增加模块
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="serviceProvider"></param>
        private void AddModule<TModule>(IServiceProvider serviceProvider)
            where TModule : IModule
        {
            var module = serviceProvider.GetRequiredService<TModule>();
            Modules.Add(module);
        }
        #endregion
    }
}
