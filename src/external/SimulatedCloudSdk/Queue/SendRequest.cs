using System;

namespace SimulatedCloudSdk.Queue
{
    public class SendRequest
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public string Body { get; set; }
    }
}
