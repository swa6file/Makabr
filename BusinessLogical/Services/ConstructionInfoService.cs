using System.Linq;
using BusinessLogical.Interfaces;
using BusinessLogical.Models;
using Model;
using DataAccessLayer;

namespace BusinessLogical.Services
{
    public class ConstructionInfoService : IConstructionInfoService
    {
        private readonly IRepository<Worker> _repository;

        public ConstructionInfoService(IRepository<Worker> repository)
        {
            _repository = repository;
        }

        public ConstructionInfo GetConstructionInfo()
        {
            var workers = _repository.ReadAll().ToList();

            return new ConstructionInfo
            {
                TotalSalaryExpenses = workers.Sum(w => w.Salary),
                ElectriciansCount = workers.Count(w => w.Specialization == Specialization.Eletrecian),
                PaintersCount = workers.Count(w => w.Specialization == Specialization.Painter),
                CraneOperatorsCount = workers.Count(w => w.Specialization == Specialization.CraneOperator),
                GeneralWorkersCount = workers.Count(w => w.Specialization == Specialization.GeneralWorker)
            };
        }
    }
}