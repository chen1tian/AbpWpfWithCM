using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpWpfWithCM.Application.Jwt.RefreshTokenStores
{
    /// <summary>
    /// 刷新令牌存储
    /// </summary>
    public interface IRefreshTokenStore : ITransientDependency
    {
        /// <summary>
        /// 添加刷新Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        Task SaveAsync(Guid userId, string refreshToken, DateTime expireTime);

        /// <summary>
        /// 获取刷新Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<RefreshTokenDto> GetAsync(Guid userId, string refreshToken);

        /// <summary>
        /// 移除用户的刷新Token
        /// </summary>
        /// <param name="refreshToken"></param>        
        /// <returns></returns>
        Task RemoveAsync(string refreshToken);
    }
}
