using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseTimeGJ.Application.Login
{
    /// <summary>
    /// 刷新Token输入
    /// </summary>
    public class RefreshTokenInput
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        [Required(ErrorMessage = "AccessToken不能为空")]
        public string AccessToken { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        [Required(ErrorMessage = "RefreshToken不能为空")]
        public string RefreshToken { get; set; }
    }
}
