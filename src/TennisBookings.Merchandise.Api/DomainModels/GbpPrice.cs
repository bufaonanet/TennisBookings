using System;

namespace TennisBookings.Merchandise.Api.DomainModels
{
    public class GbpPrice
    {
        public GbpPrice(decimal gbpPrice)
        {
            if (Value < 0)
                throw new ArgumentException("Price cannot be less than zero.");

            Value = gbpPrice;
        }

        public decimal Value { get; }

        public static GbpPrice Create(decimal gbpPrice) => new GbpPrice(gbpPrice);
    }
}
