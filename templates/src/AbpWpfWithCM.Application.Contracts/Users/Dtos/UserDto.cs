using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace AbpWpfWithCM.Application.Contracts.Users.Dtos
{
    /// <summary>
    /// 用户Dto
    /// <see cref="User"/>
    /// </summary>
    public class UserDto : EntityDto<Guid>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机号是否验证
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电子邮件是否验证
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }

    }
}
