using Model;
using BusinessLogical.Models;
namespace BusinessLogical.Interfaces
{
    public interface IWorkerValidator
    {
        ValidationResult Validate(Worker worker);
        ValidationResult ValidateName(string name);
        ValidationResult ValidateAge(int age);
        ValidationResult ValidateSalary(int salary);
    }
}