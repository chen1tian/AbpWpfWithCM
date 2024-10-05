using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Users;

namespace AbpWpfWithCM.Domain.SignalR
{
    /// <summary>
    /// singalr消息中心
    /// </summary>    
    public class MsgHub : AbpHub
    {
        /// <summary>
        /// 发送消息给所有用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            var currentUser = CurrentUser.Id?.ToString();
            if (currentUser != null)
            {
                await Clients.User(currentUser).SendAsync("ReceiveMessage", currentUser, message);
            }
            else
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}
