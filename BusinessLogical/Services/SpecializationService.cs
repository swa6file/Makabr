using BusinessLogical.Interfaces;

namespace BusinessLogical.Services
{
    public class SpecializationService : ISpecializationService
    {
        public string[] GetAvailableSpecializations()
        {
            return new string[]
            {
                "Электрик",
                "Маляр",
                "Крановщик",
                "Разнорабочий"
            };
        }
    }
}