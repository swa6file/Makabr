using System.Collections.Generic;
using System.Linq;
using BusinessLogical.Interfaces;
using BusinessLogical.Models;
using Model;
using DataAccessLayer;

namespace BusinessLogical.Services
{
    public class WorkerFilterService : IWorkerFilterService
    {
        private readonly IRepository<Worker> _repository;

        public WorkerFilterService(IRepository<Worker> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Worker> FilterWorkers(WorkerFilterCriteria criteria)
        {
            var query = _repository.ReadAll().AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
                query = query.Where(w => w.Name.Contains(criteria.Name));

            if (criteria.MinAge.HasValue)
                query = query.Where(w => w.Age >= criteria.MinAge.Value);

            if (criteria.MaxAge.HasValue)
                query = query.Where(w => w.Age <= criteria.MaxAge.Value);

            if (criteria.MinSalary.HasValue)
                query = query.Where(w => w.Salary >= criteria.MinSalary.Value);

            if (criteria.MaxSalary.HasValue)
                query = query.Where(w => w.Salary <= criteria.MaxSalary.Value);

            if (criteria.Specialization.HasValue)
                query = query.Where(w => w.Specialization == criteria.Specialization.Value);

            return query.ToList();
        }
    }
}