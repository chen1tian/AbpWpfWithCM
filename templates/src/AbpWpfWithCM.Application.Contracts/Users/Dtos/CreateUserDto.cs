using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Validation;


namespace AbpWpfWithCM.Application.Contracts.Users.Dtos
{
    /// <summary>
    /// 创建用户Dto
    /// <see cref="User"/>
    /// </summary>
    public class CreateUserDto
    // :  IValidatableObject
    {
        /// <summary>
        /// 用户名
        /// </summary>        
        [StringLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码hash
        /// </summary>
        [Required(ErrorMessage = "密码必须填写")]
        [StringLength(100)]
        public string Password { get; set; }

        /// <summary>
        /// 哈希后的密码
        /// </summary>
        [JsonIgnore]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名必须填写")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>        
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [StringLength(250)]
        public string Email { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (string.IsNullOrEmpty(PhoneNumber))
        //    {
        //        yield return new ValidationResult("手机号码必须填写");
        //    }

        //    if (!ValidHelper.IsPhoneNumber(PhoneNumber))
        //    {
        //        yield return new ValidationResult("手机号码格式不正确");
        //    }
        //}
    }
}
