using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBookings.Merchandise.Api.Data.Dto;

namespace TennisBookings.Merchandise.Api.External.Database
{
    /// <summary>
    /// An abstraction around the external cloud database SDK from SomeCloudProvider™
    /// </summary>
    public interface ICloudDatabase
    {
        Task<ProductDto> GetAsync(string id);        
        Task InsertAsync(string id, ProductDto product);
        Task<IReadOnlyCollection<ProductDto>> ScanAsync();
    }
}
