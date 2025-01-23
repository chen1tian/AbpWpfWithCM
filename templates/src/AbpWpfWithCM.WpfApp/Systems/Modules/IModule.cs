using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpWpfWithCM.WpfApp.Systems.Modules
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IModule : ISingletonDependency
    {
        /// <summary>
        /// 展示名称
        /// </summary>

        string DisplayName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        PackIconKind Icon { get; set; }
    }
}
