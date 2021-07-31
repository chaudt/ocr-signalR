using System.Threading.Tasks;

namespace OCR_SignalR.Services
{
    public interface IHubWrapper
    {
        Task PublishToGroupAsync(string groupName, string message, object data);
        Task PublishToAllAsync(string message, object data);
    }
}