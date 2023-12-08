using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PudgeSMomom.Models;

namespace PudgeSMomom.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;
        public ChatHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        //public async Task SendMessage(string message, Dialogue dialogue)
        //{
        //    //await Clients.All.SendAsync("Receive", message);
        //    var initiator = await _userManager.FindByIdAsync(dialogue.InitiatiorId);
        //    dialogue.Messages.Add(new UserMessage { Data = message, NickName = initiator.UserName});
        //    await Clients.User(dialogue.RecieverId).SendAsync("Receive", message);
        //}
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }
    }
}
