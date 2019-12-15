using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TestExamen.Hubs
{
    public class CommentHub: Hub
    {

        public class Data
        {
            public string message { get; set; }
            public string user { get; set; } = "A socket";
            public string color { get; set; }
        }
        public async Task ClientMessage(Data data)
        {
            await Clients.All.SendAsync("ServerMessage", new { message = $"{data.user} says: {data.message}!", user = data.user, data.color });
        }

        public async Task Login(string user)
        {
            var randomColor = String.Format("#{0:X6}", new Random().Next(0x1000000));
            await Clients.Caller.SendAsync("ServerMessage", new { userName = user, message = $"Welcome {user}!", color = randomColor });
            await Clients.Others.SendAsync("ServerMessage", new { userName = user, message = $"{user} just logged in.", color = randomColor });

        }
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("Login",
                 new { message = $"Welcome {Context.ConnectionId}!" });
            await base.OnConnectedAsync();
        }

        //Hub events ---------------------- 
        public async Task OnDisConnectedAsync()
        {
            var user = this.Clients.Caller;
            await Clients.Others.SendAsync("ServerMessage", new { message = $"{user} is loggedout." });
        }
    }
    
}
