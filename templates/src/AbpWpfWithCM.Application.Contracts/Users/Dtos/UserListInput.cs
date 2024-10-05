using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace AbpWpfWithCM.Application.Contracts.Users.Dtos
{
    /// <summary>
    /// 查询用户列表输入
    /// </summary>
    public class UserListInput : PagedResultRequestDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
