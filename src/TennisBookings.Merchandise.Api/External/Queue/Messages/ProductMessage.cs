using System;

namespace TennisBookings.Merchandise.Api.External.Queue.Messages
{
    internal class ProductMessage
    {
        public string EventName { get; set; }
        public Guid ProductId { get; set; }
        public DateTime EventDate { get; set; }
    }
}
