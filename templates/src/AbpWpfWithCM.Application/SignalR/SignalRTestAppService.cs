using AbpWpfWithCM.Application.Contracts.SignalR;
using AbpWpfWithCM.Domain.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace AbpWpfWithCM.Application.SignalR
{
    public class SignalRTestAppService : ApplicationService, ISignalRTestAppService
    {
        private readonly IHubContext<MsgHub> _hubContext;

        public SignalRTestAppService(IHubContext<MsgHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendHelloAsync()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Hello from the SignalRTestAppService!");
        }

        [Authorize]
        public async Task SendHelloToUserAsync()
        {
            var userId = CurrentUser.Id;
            var userName = CurrentUser.UserName;

            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveMessage", $"Hello from the SignalRTestAppService to user {userName}!");
        }
    }
}
