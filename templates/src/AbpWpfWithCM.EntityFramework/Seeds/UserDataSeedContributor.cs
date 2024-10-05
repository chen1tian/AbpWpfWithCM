using AbpWpfWithCM.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;


namespace CruiseTimeGJ.EntityFramework.Seeds
{
    /// <summary>
    /// 用户数据种子贡献者
    /// </summary>
    public class UserDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<User, Guid> _userRepository;

        public UserDataSeedContributor(IRepository<User, Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _userRepository.GetCountAsync() == 0)
            {
                var password = "123.123a";
                var passwordHash = Crypto.HashPassword(password);
                var user = User.Create("admin", passwordHash, "管理员");
                user.IsSuperAdmin = true;

                await _userRepository.InsertAsync(user, true);
            }
        }
    }
}
