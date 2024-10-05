using AbpWpfWithCM.Domain.Domains;
using AbpWpfWithCM.EntityFramework;
using CruiseTimeGJ.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbpWpfWithCM.Application.Jwt.RefreshTokenStores
{
    /// <summary>
    /// 刷新令牌存储
    /// </summary>
    public class RefreshTokenStore : EfCoreRepository<AbpWpfWithCMDbContext, RefreshToken, Guid>, IRefreshTokenStore
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RefreshTokenStore(IDbContextProvider<AbpWpfWithCMDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<RefreshTokenDto> GetAsync(Guid userId, string refreshToken)
        {
            var entity = await FindAsync(x => x.UserId == userId && x.Token == refreshToken);

            if (entity == null)
            {
                return null;
            }

            return new RefreshTokenDto
            {
                UserId = entity.UserId,
                Token = entity.Token,
                ExpireTime = entity.ExpireTime
            };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string refreshToken)
        {
            await DeleteAsync(x => x.Token == refreshToken);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public async Task SaveAsync(Guid userId, string refreshToken, DateTime expireTime)
        {
            var entity = new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                ExpireTime = expireTime
            };

            await InsertAsync(entity);
        }
    }
}
