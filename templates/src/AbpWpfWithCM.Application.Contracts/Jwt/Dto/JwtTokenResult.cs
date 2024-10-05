using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Application.Contracts.Jwt.Dto
{
    /// <summary>
    /// Jwt结果
    /// </summary>    
    public class JwtTokenResult
    {
        /// <summary>
        /// 访问Token
        /// </summary>
        [JsonProperty("access_token")]
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// RefreshToken 刷新令牌
        /// </summary>
        [JsonProperty("refresh_token")]
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 访问Token过期时间 单位秒钟
        /// </summary>
        [JsonProperty("expires_in")]
        [JsonPropertyName("expires_in")]
        public int AccessExpiration { get; set; }
    }
}
