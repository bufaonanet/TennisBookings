using System.Collections.Generic;
using System.Threading.Tasks;
using SimulatedCloudSdk.Database;
using TennisBookings.Merchandise.Api.Data.Dto;

namespace TennisBookings.Merchandise.Api.External.Database
{
    public class CloudDatabase : ICloudDatabase
    {
        private readonly IDatabaseClient<ProductDto> _client;

        public CloudDatabase(IDatabaseClient<ProductDto> client)
        {
            _client = client;
        }

        public Task<ProductDto> GetAsync(string id) => _client.GetAsync(id);

        public Task InsertAsync(string id, ProductDto product) => _client.InsertAsync(id, product);

        public Task<IReadOnlyCollection<ProductDto>> ScanAsync() => _client.ScanAsync();
    }
}
