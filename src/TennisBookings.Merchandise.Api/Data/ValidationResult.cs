using System.Collections.Generic;
using System.Linq;

namespace TennisBookings.Merchandise.Api.Data
{
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public ErrorsList Errors { get; set; } = new ErrorsList();
    }
}
