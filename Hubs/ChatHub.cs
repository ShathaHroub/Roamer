using Microsoft.AspNetCore.SignalR;

namespace Roamer.Hubs
{
    public class ChatHub: Hub
    {
        //sendmessage client method and receivemessage server side method content has to be the same
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
