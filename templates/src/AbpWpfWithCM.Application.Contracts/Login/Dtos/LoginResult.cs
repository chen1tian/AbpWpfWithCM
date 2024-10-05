using AbpWpfWithCM.Application.Contracts.Jwt.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Application.Contracts.Login.Dtos
{
    public class LoginResult
    {
        public string status { get; set; }
        public string type { get; set; }
        public JwtTokenResult token { get; set; }
    }
}
