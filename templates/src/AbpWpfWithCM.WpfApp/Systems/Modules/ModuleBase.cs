using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.WpfApp.Systems.Modules
{
    public class ModuleBase : Screen, ITransientModule
    {
        public ModuleBase(string displayName, PackIconKind icon)
        {
            DisplayName = displayName;
            Icon = icon;
        }

        /// <summary>
        /// 图标
        /// </summary>
        public PackIconKind Icon { get; set; }
    }
}
