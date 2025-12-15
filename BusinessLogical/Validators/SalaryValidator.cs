using BusinessLogical.Interfaces;
using BusinessLogical.Models;

namespace BusinessLogical.Validators
{
    public class SalaryValidator : ISalaryValidator
    {
        public ValidationResult Validate(int salary)
        {
            if (salary < 25000 || salary > 1000000)
                return ValidationResult.Invalid("Зарплата должна быть от 25,000 до 1,000,000");

            return ValidationResult.Valid();
        }
    }
}