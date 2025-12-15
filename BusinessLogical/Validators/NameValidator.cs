using System.Linq;
using BusinessLogical.Interfaces;
using BusinessLogical.Models;

namespace BusinessLogical.Validators
{
    public class NameValidator : INameValidator
    {
        public ValidationResult Validate(string name)
        {
            char[] forbiddenChars = { '!', '@', '#', '$', '%', '&', '*', '(', ')', '_', '+', '=',
                                    '[', ']', '{', '}', '|', '\\', '/', '<', '>', '?', ';', ':',
                                    '"', '\'', '№' };

            if (string.IsNullOrWhiteSpace(name))
                return ValidationResult.Invalid("Имя не может быть пустым");

            if (forbiddenChars.Any(c => name.Contains(c)) || name.Any(char.IsDigit))
                return ValidationResult.Invalid("Имя содержит запрещенные символы или цифры");

            return ValidationResult.Valid();
        }
    }
}