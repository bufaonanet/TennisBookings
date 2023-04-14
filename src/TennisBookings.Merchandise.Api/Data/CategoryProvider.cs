using System.Collections.Generic;

namespace TennisBookings.Merchandise.Api.Data
{
    public class CategoryProvider : ICategoryProvider
    {
        public IReadOnlyCollection<string> AllowedCategories()
        {
            var allowedCategories = new string[]
            {
                "Accessories",
                "Bags",
                "Balls",
                "Clothing",
                "Rackets"
            };

            return allowedCategories;
        }
    }
}
