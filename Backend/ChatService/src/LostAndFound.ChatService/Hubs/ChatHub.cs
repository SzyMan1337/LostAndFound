using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace LostAndFound.ChatService.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is not null)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is not null)
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
