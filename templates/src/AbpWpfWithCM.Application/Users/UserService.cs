using AbpWpfWithCM.Application.Contracts.Users;
using AbpWpfWithCM.Application.Contracts.Users.Dtos;
using AbpWpfWithCM.Domain.ApiResults;
using AbpWpfWithCM.Domain.Config;
using AbpWpfWithCM.Domain.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;


namespace CruiseTimeGJ.Application.Users
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [WrapResult]
    [Authorize(Policy = Consts.Policys.SuperAdmin)]
    public class UserService : CrudAppService<User, UserDto, Guid, UserListInput, CreateUserDto, UpdateUserDto>, IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userRepo"></param>
        /// <param name="dataFilter"></param>
        public UserService(IRepository<User, Guid> repository) : base(repository)
        {

        }

        /// <summary>
        /// 创建过滤查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override async Task<IQueryable<User>> CreateFilteredQueryAsync(UserListInput input)
        {
            var query = await Repository.GetQueryableAsync();

            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Name), x => x.Name.Contains(input.Name))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserName), x => x.UserName.Contains(input.UserName))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNumber), x => x.PhoneNumber.Contains(input.PhoneNumber))
                ;

            return query;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            // 检查用户名是否存在
            if (!string.IsNullOrWhiteSpace(input.UserName) && await Repository.AnyAsync(x => x.UserName == input.UserName))
            {
                throw new UserFriendlyException("用户名已经存在");
            }

            // 检查手机号是否存在
            if (!string.IsNullOrWhiteSpace(input.PhoneNumber) && await Repository.AnyAsync(x => x.PhoneNumber == input.PhoneNumber))
            {
                throw new UserFriendlyException("手机号已经存在");
            }

            // 哈希密码            
            input.PasswordHash = Crypto.HashPassword(input.Password);
            return await base.CreateAsync(input);
        }

        public override async Task<PagedResultDto<UserDto>> GetListAsync(UserListInput input)
        {
            var users = await base.GetListAsync(input);
            return users;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto input)
        {
            var user = await Repository.FirstOrDefaultAsync(x => x.Id == id);

            // 如果用户不存在
            if (user == null)
            {
                throw new UserFriendlyException("用户不存在");
            }

            // 检查用户名是否存在
            if (!string.IsNullOrWhiteSpace(input.UserName) && await Repository.AnyAsync(x => x.Id != input.Id && x.UserName == input.UserName))
            {
                throw new UserFriendlyException("用户名已经存在");
            }

            // 检查手机号是否存在
            if (!string.IsNullOrWhiteSpace(input.PhoneNumber) && await Repository.AnyAsync(x => x.Id != input.Id && x.PhoneNumber == input.PhoneNumber))
            {
                throw new UserFriendlyException("手机号已经存在");
            }

            await CheckUpdatePolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            // 如果手机号码变动，那么需要重新验证
            entity.ValidPhoneNumber(input.PhoneNumber);

            await MapToEntityAsync(input, entity);
            await Repository.UpdateAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }

        /// <summary>
        /// 修改自己的密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> ChangeSelfPassword(ChangeSelfPasswordInput input)
        {
            var userId = CurrentUser.Id;

            var user = await Repository.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new UserFriendlyException("用户不存在");
            }

            // 检查旧密码是否正确
            var oldPwdIsValid = Crypto.VerifyHashedPassword(user.PasswordHash, input.OldPassword);
            if (!oldPwdIsValid)
            {
                throw new UserFriendlyException("旧密码不正确");
            }

            // 哈希密码
            var newPasswordHash = Crypto.HashPassword(input.NewPassword);

            user.ChangePassword(newPasswordHash);
            // 更新数据库
            await Repository.UpdateAsync(user);
            return true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> ChangePassword(ChangePasswordInput input)
        {
            var user = await Repository.FirstOrDefaultAsync(x => x.Id == input.Id);

            // 如果用户不存在
            if (user == null)
            {
                throw new UserFriendlyException("用户不存在");
            }

            // 哈希密码
            var newPasswordHash = Crypto.HashPassword(input.Password);

            user.ChangePassword(newPasswordHash);

            // 更新数据库
            await Repository.UpdateAsync(user);

            return true;
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="validCode"></param>
        /// <returns></returns>
        /// <summary>
        /// 手机号码验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> ValidPhoneNumber(ValidPhoneNumberInput input)
        {
            var query = await Repository.GetQueryableAsync();

            var user = await query.FirstOrDefaultAsync(u => u.Id == input.UserId && u.PhoneNumber == input.PhoneNumber);

            if (user == null)
            {
                throw new UserFriendlyException("用户不存在");
            }

            if (user.PhoneNumberConfirmed)
            {
                throw new UserFriendlyException("手机号码已经验证过了");
            }

            if (input.ValidCode == "1234")
            {
                // 手机号码验证成功
                user.PhoneNumberConfirme();

                await Repository.UpdateAsync(user);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
