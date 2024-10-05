using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Application.Contracts.Users.Dtos
{
    /// <summary>
    /// 手机号码验证输入
    /// </summary>
    public class ValidPhoneNumberInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidCode { get; set; }
    }
}
