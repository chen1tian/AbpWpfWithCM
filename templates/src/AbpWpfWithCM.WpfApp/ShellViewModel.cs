using AbpWpfWithCM.Domain.Localization.Resources;
using Caliburn.Micro;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
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

        public IStringLocalizer<AbpWpfWithCMResources> L => _localizer;

        public ShellViewModel(IStringLocalizer<AbpWpfWithCMResources> localizer)
        {
            _localizer = localizer;
        }
    }
}
