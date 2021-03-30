using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(Message message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
