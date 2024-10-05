using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;


namespace AbpWpfWithCM.Application.Contracts.Users.Dtos
{
    /// <summary>
    /// 更新用户Dto
    /// </summary>
    public class UpdateUserDto : EntityDto<Guid>
    //, IValidatableObject
    {
        /// <summary>
        /// 用户名
        /// </summary>        
        [StringLength(100)]
        public string UserName { get; set; }

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
