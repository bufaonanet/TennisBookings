using System.Collections.Generic;

namespace TennisBookings.Merchandise.Api.Data
{
    public class ErrorsList : List<KeyValuePair<string, string>>
    {
        public void Add(string key, string value) => Add(new KeyValuePair<string, string>(key, value));
    }
}
