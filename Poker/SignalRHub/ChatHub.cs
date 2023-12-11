using Microsoft.AspNetCore.SignalR;

namespace Poker.SignalR
{
    public class ChatHub : Hub
    {
        public async Task Enter(string userName, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.All.SendAsync("Notify",$"{userName} enter in group {groupName}");
        }
        public async Task SendMessage(string message,string userName,string groupName)
        {
            await Clients.Group(groupName).SendAsync("Receive",message,userName);
        }
    }
}
