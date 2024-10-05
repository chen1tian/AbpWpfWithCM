using AbpWpfWithCM.Application.Contracts.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;


namespace AbpWpfWithCM.Application.Contracts.Users
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService : ICrudAppService<UserDto, Guid, UserListInput, CreateUserDto, UpdateUserDto>
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ChangePassword(ChangePasswordInput input);

        /// <summary>
        /// 修改自己的密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ChangeSelfPassword(ChangeSelfPasswordInput input);

        /// <summary>
        /// 手机号码验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ValidPhoneNumber(ValidPhoneNumberInput input);
    }
}
