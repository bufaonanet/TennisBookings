using System.Threading.Tasks;
using SimulatedCloudSdk.Queue;

namespace TennisBookings.Merchandise.Api.External.Queue
{
    /// <summary>
    /// An abstraction around the external cloud queue SDK from SomeCloudProvider™
    /// </summary>
    public interface ICloudQueue
    {
        /// <summary>
        /// Sends a message to the queue. This wraps the functionality offered by 
        /// the cloud queue SDK from SomeCloudProvider™
        /// </summary>
        /// <param name="request">The <see cref="SendRequest"/> to send.</param>
        /// <returns>A <see cref="SendResponse"/> from the cloud queue service.</returns>
        Task<SendResponse> SendAsync(SendRequest request);
    }
}
