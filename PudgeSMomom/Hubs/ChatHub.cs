using Microsoft.AspNetCore.SignalR;

namespace PudgeSMomom.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SandMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            //await Clients.User("asd").SendAsync(user, message);
        }
        public override Task OnConnectedAsync()
        {
            // Получение Connection ID при подключении клиента
            var connectionId = Context.ConnectionId;
            // Дополнительная логика, если необходимо

            return base.OnConnectedAsync();
        }
    }
}
