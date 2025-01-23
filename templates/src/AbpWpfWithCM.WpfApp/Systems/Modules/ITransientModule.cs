using AbpWpfWithCM.WpfApp.Systems.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpWpfWithCM.WpfApp.Systems.Modules
{
    public interface ITransientModule : IModule, ITransientDependency
    {
    }
}
