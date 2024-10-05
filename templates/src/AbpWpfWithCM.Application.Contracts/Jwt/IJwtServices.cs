using AbpWpfWithCM.Application.Contracts.Jwt.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace AbpWpfWithCM.Application.Contracts.Jwt
{
    /// <summary>
    /// Jwt 服务
    /// </summary>
    public interface IJwtServices : ITransientDependency
    {
        /// <summary>
        /// 请求Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        Task<JwtTokenResult> RequestTokenAsync(Guid userId, IEnumerable<Claim> claims);

        /// <summary>
        /// 请求Token
        /// </summary>
        /// <param name="user"></param>        
        /// <param name="payloads"></param>        
        /// <returns></returns>
        Task<JwtTokenResult> RequestTokenAsync(JwtUserDto user, params Claim[] payloads);

        /// <summary>
        /// 获取UserId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string GetUserId(string token);

        /// <summary>
        /// 获取负载
        /// </summary>
        /// <param name="accessToken"></param>
        JwtSecurityToken GetPayload(string accessToken);

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<JwtTokenResult> RefreshAsync(string accessToken, string refreshToken);

        /// <summary>
        /// 删除刷新Token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task RemoveRefreshTokenAsync(string refreshToken);
    }
}
