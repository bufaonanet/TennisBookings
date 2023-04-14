using Microsoft.AspNetCore.Mvc.ModelBinding;
using TennisBookings.Merchandise.Api.Data;

namespace TennisBookings.Merchandise.Api.Core
{
    public static class ModelStateExtensions
    {
        public static ModelStateDictionary AddValidationResultErrors(this ModelStateDictionary modelState, ErrorsList errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Key, error.Value);
            }

            return modelState;
        }
    }
}
