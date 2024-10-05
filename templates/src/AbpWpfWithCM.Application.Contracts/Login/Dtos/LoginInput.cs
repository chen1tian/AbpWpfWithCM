using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Application.Contracts.Login.Dtos
{
    public class LoginInput
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool autoLogin { get; set; }
        public string type { get; set; }
    }

}
