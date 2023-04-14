namespace TennisBookings.Merchandise.Api.Data
{
    public class AddProductResult
    {
        public AddProductResult(ValidationResult validationResult, bool isDuplicate)
        {
            ValidationResult = validationResult;
            IsDuplicate = isDuplicate;
        }

        public bool IsSuccess => IsValid && !IsDuplicate;
        public ValidationResult ValidationResult { get; }
        public bool IsValid => ValidationResult.IsValid;
        public bool IsDuplicate { get; }
    }
}
