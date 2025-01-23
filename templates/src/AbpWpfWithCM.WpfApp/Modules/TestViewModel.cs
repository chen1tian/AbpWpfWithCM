using AbpWpfWithCM.WpfApp.Systems.Modules;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.WpfApp.Modules
{
    [AddINotifyPropertyChangedInterface]
    public class TestViewModel : ModuleBase, ITransientModule
    {
        public TestViewModel() : base("测试", PackIconKind.TestTube)
        {

        }
    }
}
