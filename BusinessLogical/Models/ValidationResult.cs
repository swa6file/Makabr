namespace BusinessLogical.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string ErrorMessage { get; }

        public ValidationResult(bool isValid, string errorMessage = "")
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult Valid() => new ValidationResult(true);
        public static ValidationResult Invalid(string message) => new ValidationResult(false, message);
    }
}