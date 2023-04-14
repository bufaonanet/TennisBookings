using System;

namespace TennisBookings.Web.Services
{
    public class SystemDateTime : IDateTime
    {
        public DateTime DateTimeUtc => DateTime.UtcNow;
    }
}
