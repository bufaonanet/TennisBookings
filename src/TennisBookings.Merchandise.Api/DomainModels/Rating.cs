using System;

namespace TennisBookings.Merchandise.Api.DomainModels
{
    public struct Rating : IComparable<Rating>, IEquatable<Rating>
    {
        public Rating(int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            Score = rating;
        }

        public int Score { get; }

        public bool Equals(Rating other) => Score.Equals(other.Score);
        public int CompareTo(Rating other) => Score.CompareTo(other.Score);

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            return obj is Rating other && Equals(other);
        }

        public static bool operator ==(Rating a, Rating b) => a.CompareTo(b) == 0;
        public static bool operator !=(Rating a, Rating b) => !(a == b);
        public override int GetHashCode() => Score.GetHashCode();
        public override string ToString() => Score.ToString();

        public static Rating Create(int rating) => new Rating(rating);
    }
}
