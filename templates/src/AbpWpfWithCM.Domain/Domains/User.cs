using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AbpWpfWithCM.Domain.Domains
{
    public class User : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码hash
        /// </summary>
        [StringLength(100)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机号是否验证
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; } = true;

        /// <summary>
        /// 电子邮件
        /// </summary>
        [StringLength(250)]
        public string Email { get; set; }

        /// <summary>
        /// 电子邮件是否验证
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static User Create(string username, string passwordHash, string name)
        {
            var user = new User()
            {
                UserName = username,
                PasswordHash = passwordHash,
                Name = name,
            };

            return user;
        }

        /// <summary>
        /// 手机号码已验证
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void PhoneNumberConfirme()
        {
            PhoneNumberConfirmed = true;
        }

        /// <summary>
        /// 验证手机
        /// 如果手机变动过，那么需要重新验证
        /// </summary>
        /// <param name="phoneNumber"></param>
        public void ValidPhoneNumber(string phoneNumber)
        {
            //if (this.PhoneNumber != phoneNumber)
            //{
            //    this.PhoneNumberConfirmed = false;
            //}
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="newPasswordHash"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }
    }
}
