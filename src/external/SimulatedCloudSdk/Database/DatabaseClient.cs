using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedCloudSdk.Database
{
    public class DatabaseClient<T> : IDatabaseClient<T>
    {
        private readonly Dictionary<string, T> _items = new Dictionary<string, T>();

        public async Task InsertAsync(string id, T product)
        {
            await Task.Delay(100);

            _items[id] = product;
        }

        public async Task<IReadOnlyCollection<T>> ScanAsync()
        {
            await Task.Delay(250);

            return _items.Values;
        }

        public async Task<T> GetAsync(string id)
        {
            await Task.Delay(100);

            return _items.TryGetValue(id, out var value) ? value : default;
        }
    }
}
