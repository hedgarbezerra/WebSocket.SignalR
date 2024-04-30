using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebSocket.SignalR.Hubs
{
    [Authorize]
    public class BuyingTicketHub : Hub
    {
    }
}
