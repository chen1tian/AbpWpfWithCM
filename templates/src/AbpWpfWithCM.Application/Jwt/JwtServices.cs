using AbpWpfWithCM.Application.Contracts.Jwt;
using AbpWpfWithCM.Application.Contracts.Jwt.Dto;
using AbpWpfWithCM.Application.Jwt.RefreshTokenStores;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using Volo.Abp;
using Volo.Abp.Security.Claims;

namespace AbpWpfWithCM.Application.Jwt
{
    /// <summary>
    /// Jwt服务
    /// </summary>
    public class JwtServices : IJwtServices, ITransientDependency
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IRefreshTokenStore _refreshTokenStore;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="jwtOptions"></param>
        /// <param name="refreshTokenStore"></param>
        public JwtServices(IOptions<JwtOptions> jwtOptions, IRefreshTokenStore refreshTokenStore)
        {
            _jwtOptions = jwtOptions.Value;
            _refreshTokenStore = refreshTokenStore;
        }

        /// <summary>
        /// 请求Token
        /// </summary>        
        /// <returns></returns>
        public async Task<JwtTokenResult> RequestTokenAsync(Guid userId, IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims,
                expires: DateTime.Now.AddSeconds(_jwtOptions.AccessExpiration),
                signingCredentials: credentials);

            // 产生Token
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            // 产生刷新Token
            var refreshToken = Guid.NewGuid().ToString("N");

            var result = new JwtTokenResult
            {
                AccessToken = token,
                AccessExpiration = _jwtOptions.AccessExpiration,
                RefreshToken = refreshToken
            };

            // 保存刷新Token
            var rtExpires = DateTime.Now.AddSeconds(_jwtOptions.RefreshExpiration);
            await _refreshTokenStore.SaveAsync(userId, refreshToken, rtExpires);

            return result;
        }

        /// <summary>
        /// 请求Token
        /// </summary>        
        /// <returns></returns>
        public async Task<JwtTokenResult> RequestTokenAsync(JwtUserDto user, params Claim[] payloads)
        {
            if (user.Id == null)
            {
                throw new Exception("用户Id不能为空");
            }

            // 用户名为空时，使用手机号码
            user.UserName = user.UserName ?? user.PhoneNumber;

            var claims = new List<Claim> {
                // 注意：对于abp框架来说，两个claim都需要声明，才能自动给CurrentUser赋值
                // 另外：abp框架中，UserId是Guid类型，类型不对会导致CurrentUser为null
                new Claim(AbpClaimTypes.UserId, user.Id.ToString()),
                new Claim(AbpClaimTypes.Name, user.Name),
                new Claim(AbpClaimTypes.UserName, user.UserName),
                new Claim(AbpClaimTypes.PhoneNumber, user.PhoneNumber ?? string.Empty),
            };

            // 添加负载
            if (payloads != null)
            {
                claims.AddRange(payloads);
            }

            var result = await RequestTokenAsync(user.Id.Value, claims);

            return result;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="user"></param>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<JwtTokenResult> RefreshAsync(string accessToken, string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new UserFriendlyException("AccessToken不能为空");

            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new UserFriendlyException("refreshToken不能为空");

            // 从accessToken中得到负载
            var payload = GetPayload(accessToken);
            var claims = payload.Claims;

            var userId = GetUserId(accessToken);
            var gUserId = Guid.Parse(userId);

            var tokenEntity = await _refreshTokenStore.GetAsync(gUserId, refreshToken);

            // 如果为空则异常
            if (tokenEntity == null)
            {
                // 抛出401错误
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            // 检查过期时间
            if (tokenEntity.ExpireTime < DateTime.Now)
            {
                // 删除旧Token
                await _refreshTokenStore.RemoveAsync(refreshToken);
                // 抛出401错误
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            // 检查通过的话，生成新的Token
            var token = await RequestTokenAsync(gUserId, claims);

            // 删除旧Token
            await _refreshTokenStore.RemoveAsync(refreshToken);

            return token;
        }

        /// <summary>
        /// 获取UserId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetUserId(string token)
        {
            var payload = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = payload.Claims.FirstOrDefault(x => x.Type == AbpClaimTypes.UserId)?.Value;
            return userId;
        }

        /// <summary>
        /// 获取负载
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public JwtSecurityToken GetPayload(string accessToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
        }

        /// <summary>
        /// 删除刷新Token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task RemoveRefreshTokenAsync(string refreshToken)
        {
            await _refreshTokenStore.RemoveAsync(refreshToken);
        }
    }
}
