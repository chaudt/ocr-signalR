using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using OCR_SignalR.Hubs;

namespace OCR_SignalR.Services
{
    public class HubWrapper : IHubWrapper
    {
        private readonly IHubContext<RCMSHub> _hubContext;

        public HubWrapper(IHubContext<RCMSHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task PublishToAllAsync(string message, object data)
            => await _hubContext.Clients.All.SendAsync(message, data);

        public async Task PublishToGroupAsync(string groupName, string message, object data)
         => await _hubContext.Clients.Group(groupName).SendAsync(message, data);
    }
}