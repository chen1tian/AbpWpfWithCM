using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Domain.ApiResults
{
    /// <summary>
    /// 包装结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WrapResult<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WrapResult()
        {
            Success = true;
            ErrorMessage = "Success";
            Data = default;
            ErrorCode = 200;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 消息代码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 设置成功结果
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public void SetSuccess(T data, string message = "Success", int code = 0)
        {
            Success = true;
            Data = data;
            ErrorCode = code;
        }

        /// <summary>
        /// 设置失败结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public void SetFail(string message = "Fail", int code = 500)
        {
            Success = false;
            ErrorMessage = message;
            ErrorCode = code;
        }
    }
}
