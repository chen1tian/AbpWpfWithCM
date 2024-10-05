using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace AbpWpfWithCM.Domain.SignalR
{
    public class SignalRUserIdProvider : AbpSignalRUserIdProvider
    {
        public SignalRUserIdProvider(ICurrentPrincipalAccessor currentPrincipalAccessor, ICurrentUser currentUser) : base(currentPrincipalAccessor, currentUser)
        {
        }

        public override string GetUserId(HubConnectionContext connection)
        {
            var userId = connection.User.FindFirst(AbpClaimTypes.UserId)?.Value;
            return userId;
        }
    }
}
