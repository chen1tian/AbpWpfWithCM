using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Application.Contracts.Users.Dtos
{
    /// <summary>
    /// 修改密码输入
    /// </summary>
    public class ChangePasswordInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string Password { get; set; }
    }
}
