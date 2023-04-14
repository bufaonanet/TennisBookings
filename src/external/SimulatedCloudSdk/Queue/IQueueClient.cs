using System.Threading.Tasks;

namespace SimulatedCloudSdk.Queue
{
    public interface IQueueClient
    {
        Task<SendResponse> Send(SendRequest request);
    }
}
