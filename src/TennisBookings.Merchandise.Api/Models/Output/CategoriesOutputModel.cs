using System.Collections.Generic;

namespace TennisBookings.Merchandise.Api.Models.Output
{
    public class CategoriesOutputModel
    {
        public CategoriesOutputModel(IReadOnlyCollection<string> categories)
        {
            AllowedCategories = categories;
        }

        public IReadOnlyCollection<string> AllowedCategories { get; }
    }
}
