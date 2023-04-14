using System.Text.Json;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Helpers.Serialization
{
    public static class JsonSerializerHelper
    {
        public static JsonSerializerOptions DefaultSerialisationOption =>
            new JsonSerializerOptions { IgnoreNullValues = true };

        public static JsonSerializerOptions DefaultDeserialisationOption =>
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
}
