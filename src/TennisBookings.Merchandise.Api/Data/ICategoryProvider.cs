using System.Collections.Generic;

namespace TennisBookings.Merchandise.Api.Data
{
    public interface ICategoryProvider
    {
        IReadOnlyCollection<string> AllowedCategories();
    }
}