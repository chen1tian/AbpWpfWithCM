using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Application.Contracts.Jwt.Dto
{
    /// <summary>
    /// JwtToken参数
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 签发者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// AccessToken过期时间 单位：秒钟
        /// </summary>
        public int AccessExpiration { get; set; }
        /// <summary>
        /// RefreshToken过期时间 单位：秒钟
        /// </summary>
        public int RefreshExpiration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }
    }
}
