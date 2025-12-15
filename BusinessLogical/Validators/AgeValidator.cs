using BusinessLogical.Interfaces;
using BusinessLogical.Models;

namespace BusinessLogical.Validators
{
    public class AgeValidator : IAgeValidator
    {
        public ValidationResult Validate(int age)
        {
            if (age < 18 || age > 65)
                return ValidationResult.Invalid("Возраст должен быть от 18 до 65 лет");

            return ValidationResult.Valid();
        }
    }
}