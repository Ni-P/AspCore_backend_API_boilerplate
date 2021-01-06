using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Children;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class ChildbirthHub : Hub
    {
        private readonly IMediator _mediator;

        public ChildbirthHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        // clients must send events with the exact method name here
        public async Task SendChildIsBorn(Create.Command command)
        {
            // fetch user info if needed
            var username = Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var child = await _mediator.Send(command);

            // send response to clients in the same group with the given event name
            await Clients.Group("parents").SendAsync("NewFamilyMember", child);
        }

        private string GetUserName()
        {
            return Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var username = GetUserName();

            await Clients.Group(groupName).SendAsync("Send", $"{username} has joined the group");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            var username = GetUserName();

            await Clients.Group(groupName).SendAsync("Send", $"{username} has joined the group");
        }
    }
}