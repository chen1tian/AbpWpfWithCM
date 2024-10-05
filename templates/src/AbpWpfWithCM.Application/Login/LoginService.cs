using AbpWpfWithCM.Application.Contracts.Jwt;
using AbpWpfWithCM.Application.Contracts.Jwt.Dto;
using AbpWpfWithCM.Application.Contracts.Login.Dtos;
using AbpWpfWithCM.Domain.ApiResults;
using AbpWpfWithCM.Domain.Config;
using AbpWpfWithCM.Domain.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement;
using Volo.Abp.Users;


namespace CruiseTimeGJ.Application.Login
{
    public class LoginService : ApplicationService
    {
        private readonly IJwtServices _jwtServices;
        private readonly IRepository<User, Guid> _userRepo;

        public LoginService(IJwtServices jwtServices, IRepository<User, Guid> userRepo)
        {
            _jwtServices = jwtServices;
            _userRepo = userRepo;
        }

        /// <summary>
        /// 登录账户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/api/login/account")]
        [AllowAnonymous]
        public async Task<LoginResult> Account(LoginInput input)
        {
            // 到数据库验证用户名密码
            var userQuery = await _userRepo.GetQueryableAsync();
            User user = userQuery.FirstOrDefault(x => x.UserName == input.username);
            if (user == null)
            {
                return new LoginResult
                {
                    status = "error",
                    type = input.type,
                    token = null,
                };
            }

            // 验证密码
            if (!Crypto.VerifyHashedPassword(user.PasswordHash, input.password))
            {
                return new LoginResult
                {
                    status = "error",
                    type = input.type,
                    token = null,
                };
            }

            // 获取角色列表            
            var jwtUserDto = ObjectMapper.Map<User, JwtUserDto>(user);

            var adminClaim = new Claim(Consts.ClaimTypes.IsSuperAdmin, user.IsSuperAdmin.ToString());
            var token = await _jwtServices.RequestTokenAsync(jwtUserDto, adminClaim);

            return new LoginResult
            {
                status = "ok",
                type = input.type,
                token = token,
            };
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [WrapResult]
        [Authorize]
        [HttpGet("/api/currentUser")]
        public async Task<LoginDto> GetCurrentUser()
        {
            var user = CurrentUser;
            var isSuperAdmin = user.FindClaimValue(Consts.ClaimTypes.IsSuperAdmin);
            return new LoginDto()
            {
                Id = user.Id.Value,
                Name = user.Name,
                IsSuperAdmin = isSuperAdmin == true.ToString(),
            };
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [WrapResult]
        [HttpPost("/api/login/outLogin")]
        public async Task<bool> OutLogin(string refreshToken)
        {
            // 如果有刷新Token，那么就删除
            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                await _jwtServices.RemoveRefreshTokenAsync(refreshToken);
            }

            return true;
        }

        [WrapResult]
        [AllowAnonymous]
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<JwtTokenResult> RefreshAsync(RefreshTokenInput input)
        {
            return await _jwtServices.RefreshAsync(input.AccessToken, input.RefreshToken);
        }
    }
}
