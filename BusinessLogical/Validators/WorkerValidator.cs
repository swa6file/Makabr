using BusinessLogical.Interfaces;
using BusinessLogical.Models;
using Model;

namespace BusinessLogical.Validators
{
    public class WorkerValidator : IWorkerValidator
    {
        private readonly INameValidator _nameValidator;
        private readonly IAgeValidator _ageValidator;
        private readonly ISalaryValidator _salaryValidator;

        public WorkerValidator(INameValidator nameValidator, IAgeValidator ageValidator, ISalaryValidator salaryValidator)
        {
            _nameValidator = nameValidator;
            _ageValidator = ageValidator;
            _salaryValidator = salaryValidator;
        }

        public ValidationResult Validate(Worker worker)
        {
            var nameResult = _nameValidator.Validate(worker.Name);
            if (!nameResult.IsValid) return nameResult;

            var ageResult = _ageValidator.Validate(worker.Age);
            if (!ageResult.IsValid) return ageResult;

            var salaryResult = _salaryValidator.Validate(worker.Salary);
            if (!salaryResult.IsValid) return salaryResult;

            return ValidationResult.Valid();
        }

        public ValidationResult ValidateName(string name) => _nameValidator.Validate(name);
        public ValidationResult ValidateAge(int age) => _ageValidator.Validate(age);
        public ValidationResult ValidateSalary(int salary) => _salaryValidator.Validate(salary);
    }
}