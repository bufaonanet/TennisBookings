using System.Threading.Tasks;
using SimulatedCloudSdk.Queue;

namespace TennisBookings.Merchandise.Api.External.Queue
{
    /// <inheritdoc/>
    public class CloudQueue : ICloudQueue
    {
        private readonly IQueueClient _queueClient;

        public CloudQueue(IQueueClient queueClient) => _queueClient = queueClient;

        /// <inheritdoc/>
        public Task<SendResponse> SendAsync(SendRequest request) => _queueClient.Send(request);
    }
}
