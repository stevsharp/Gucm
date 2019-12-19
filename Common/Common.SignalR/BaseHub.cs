using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Common.SignalR
{
    public abstract class BaseHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public abstract Task SendMessage();

    }
}
