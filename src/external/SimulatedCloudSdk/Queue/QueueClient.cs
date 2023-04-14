using System;
using System.Threading.Tasks;

namespace SimulatedCloudSdk.Queue
{
    public class QueueClient : IQueueClient
    {
        public async Task<SendResponse> Send(SendRequest request)
        {
            await Task.Delay(150); // simulate network latency

            if (string.IsNullOrEmpty(request.Body))
                throw new ArgumentException("The request must contain a body");

            return new SendResponse { IsSuccess = true, StatusCode = 200 };
        }
    }
}
