using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AbpWpfWithCM.Application.Contracts.Login.Dtos
{
    /// <summary>
    /// 登录结果
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        private string _avatar;
        public string Avatar
        {
            get
            {
                if (_avatar == null)
                    _avatar = "./imgs/defaultAvatar.png";
                return _avatar;
            }
            set
            {
                _avatar = value;
            }
        }
    }
}
