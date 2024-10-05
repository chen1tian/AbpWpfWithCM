using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Application.Contracts.Users.Dtos
{
    /// <summary>
    /// 修改自己的密码
    /// </summary>
    public class ChangeSelfPasswordInput
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
    }
}

