using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogical.Interfaces;
using BusinessLogical.Models;
using Model;
using DataAccessLayer;

namespace BusinessLogical.Services
{
    public class Logic : ILogic
    {
        public event EventHandler<WorkerEventArgs> WorkerAdded;
        public event EventHandler<WorkerEventArgs> WorkerDeleted;
        public event EventHandler<WorkerEventArgs> WorkerUpdated;
        public event EventHandler DataChanged;

        public class WorkerEventArgs : EventArgs
        {
            public Worker Worker { get; }
            public Result Result { get; }

            public WorkerEventArgs(Worker worker, Result result)
            {
                Worker = worker;
                Result = result;
            }
        }

        private readonly IRepository<Worker> _repository;
        private readonly IWorkerValidator _workerValidator;
        private readonly IWorkerFilterService _filterService;
        private readonly IConstructionInfoService _constructionService;
        private readonly ISpecializationService _specializationService;

        public Logic(
            IRepository<Worker> repository,
            IWorkerValidator workerValidator,
            IWorkerFilterService filterService,
            IConstructionInfoService constructionService,
            ISpecializationService specializationService)
        {
            _repository = repository;
            _workerValidator = workerValidator;
            _filterService = filterService;
            _constructionService = constructionService;
            _specializationService = specializationService;
        }

        // Новая версия метода с nullable параметрами
        public Result AddWorker(string name, int? age, decimal? salary, string specializationStr)
        {
            // Базовая проверка на null
            if (string.IsNullOrEmpty(name) || !age.HasValue || !salary.HasValue || string.IsNullOrEmpty(specializationStr))
                return Result.Failure("Заполните все поля");

            // Парсинг специализации
            if (!Enum.TryParse<Specialization>(specializationStr, true, out var spec))
                return Result.Failure("Некорректная специализация");

            // Валидация через IWorkerValidator
            var validationResult = _workerValidator.Validate(new Worker
            {
                Name = name,
                Age = age.Value,
                Salary = (int)salary.Value,
                Specialization = spec
            });

            if (!validationResult.IsValid)
                return Result.Failure(validationResult.ErrorMessage);

            // Создание и сохранение рабочего
            var worker = new Worker
            {
                Name = name,
                Age = age.Value,
                Salary = (int)salary.Value,
                Specialization = spec
            };

            _repository.Add(worker);

            // Вызов события
            WorkerAdded?.Invoke(this, new WorkerEventArgs(worker, Result.Success($"Добавлен новый рабочий {name}")));
            DataChanged?.Invoke(this, EventArgs.Empty);

            return Result.Success($"Добавлен новый рабочий {name}");
        }

        public Result DeleteWorker(int id)
        {
            var worker = _repository.ReadById(id);
            if (worker != null)
            {
                _repository.Delete(id);

                // Вызов события
                WorkerDeleted?.Invoke(this, new WorkerEventArgs(worker,
                    Result.Success($"Работник {worker.Name} - {worker.Specialization} был уволен")));
                DataChanged?.Invoke(this, EventArgs.Empty);

                return Result.Success($"Работник {worker.Name} - {worker.Specialization} был уволен");
            }
            return Result.Failure("Работник с таким ID не найден");
        }

        // Новая версия метода UpdateWorker
        public Result UpdateWorker(int? id, string name, int? age, decimal? salary, string specializationStr)
        {
            if (!id.HasValue)
                return Result.Failure("ID рабочего не указан");

            var worker = _repository.ReadById(id.Value);
            if (worker == null)
                return Result.Failure("Работник с таким ID не найден");

            // Валидация имени
            if (!string.IsNullOrEmpty(name))
            {
                var nameValidation = _workerValidator.ValidateName(name);
                if (!nameValidation.IsValid)
                    return Result.Failure(nameValidation.ErrorMessage);
            }

            // Валидация возраста
            if (age.HasValue)
            {
                var ageValidation = _workerValidator.ValidateAge(age.Value);
                if (!ageValidation.IsValid)
                    return Result.Failure(ageValidation.ErrorMessage);
            }

            // Валидация зарплаты
            if (salary.HasValue)
            {
                var salaryValidation = _workerValidator.ValidateSalary((int)salary.Value);
                if (!salaryValidation.IsValid)
                    return Result.Failure(salaryValidation.ErrorMessage);
            }

            // Парсинг специализации (если указана)
            Specialization? spec = null;
            if (!string.IsNullOrEmpty(specializationStr))
            {
                if (!Enum.TryParse<Specialization>(specializationStr, true, out var parsedSpec))
                    return Result.Failure("Некорректная специализация");
                spec = parsedSpec;
            }

            // Обновление данных
            if (!string.IsNullOrEmpty(name))
                worker.Name = name;
            if (age.HasValue)
                worker.Age = age.Value;
            if (salary.HasValue)
                worker.Salary = (int)salary.Value;
            if (spec.HasValue)
                worker.Specialization = spec.Value;

            _repository.Update(worker);

            // Вызов события
            WorkerUpdated?.Invoke(this, new WorkerEventArgs(worker, Result.Success("Данные работника успешно изменены")));
            DataChanged?.Invoke(this, EventArgs.Empty);

            return Result.Success("Данные работника успешно изменены");
        }

        public Worker GetWorkerById(int workerId)
        {
            try
            {
                return _repository.ReadById(workerId);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Worker> GetAllWorkers() => _repository.ReadAll();

        public IEnumerable<Worker> FilterWorkers(WorkerFilterCriteria criteria)
            => _filterService.FilterWorkers(criteria);

        public string[] GetAvailableSpecializations()
            => _specializationService.GetAvailableSpecializations();

        public ConstructionInfo GetConstructionInfo()
            => _constructionService.GetConstructionInfo();
    }
}