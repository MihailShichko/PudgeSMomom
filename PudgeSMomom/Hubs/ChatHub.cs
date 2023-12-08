using Microsoft.AspNetCore.SignalR;
using PudgeSMomom.Models;

namespace PudgeSMomom.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message, Dialogue dialogue)
        {
            //await Clients.All.SendAsync("Receive", message);
            dialogue.Messages.Add(new UserMessage { Data = message });
            await Clients.User(dialogue.RecieverId).SendAsync("Receive", message);
        }
    }
}
