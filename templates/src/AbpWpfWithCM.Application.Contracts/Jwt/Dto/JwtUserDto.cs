using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Application.Contracts.Jwt.Dto
{
    /// <summary>
    /// Jwt用户信息
    /// </summary>
    public class JwtUserDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
