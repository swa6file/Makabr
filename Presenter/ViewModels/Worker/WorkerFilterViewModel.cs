using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogical.Interfaces;
using Presenter.ViewModels.Base;
using Presenter.DTOs;
using System.Windows.Input;

namespace Presenter.ViewModels.Worker
{
    public class WorkerFilterViewModel : ViewModelBase
    {
        private readonly ILogic _logic;
        private string _nameFilter;
        private int? _minAge;
        private int? _maxAge;
        private decimal? _minSalary;
        private decimal? _maxSalary;
        private string _specialization;
        private List<string> _specializationOptions;

        public string NameFilter
        {
            get => _nameFilter;
            set => SetField(ref _nameFilter, value);
        }

        public int? MinAge
        {
            get => _minAge;
            set => SetField(ref _minAge, value);
        }

        public int? MaxAge
        {
            get => _maxAge;
            set => SetField(ref _maxAge, value);
        }

        public decimal? MinSalary
        {
            get => _minSalary;
            set => SetField(ref _minSalary, value);
        }

        public decimal? MaxSalary
        {
            get => _maxSalary;
            set => SetField(ref _maxSalary, value);
        }

        public string Specialization
        {
            get => _specialization;
            set => SetField(ref _specialization, value);
        }

        public List<string> SpecializationOptions
        {
            get => _specializationOptions;
            set => SetField(ref _specializationOptions, value);
        }

        public ICommand ApplyFilterCommand { get; }
        public ICommand ClearFilterCommand { get; }
        public ICommand CloseCommand { get; }

        public event EventHandler FilterApplied;
        public event EventHandler FilterCleared;

        public WorkerFilterViewModel(ILogic logic)
        {
            _logic = logic;

            SpecializationOptions = new List<string>
        {
            "", // пустое значение для "Все специализации"
            "Electrician",
            "Painter",
            "CraneOperator",
            "GeneralWorker"
        };

            ApplyFilterCommand = new RelayCommand(ApplyFilter);
            ClearFilterCommand = new RelayCommand(ClearFilter);
            CloseCommand = new RelayCommand(Close);
        }

        private List<string> GetSpecializationOptions()
        {
            // Пустая строка для "Все специализации"
            var options = new List<string> { "" };

            // Получаем специализации из бизнес-логики
            var specs = _logic.GetAvailableSpecializations();
            if (specs != null)
            {
                options.AddRange(specs);
            }

            return options;
        }

        private void ApplyFilter(object parameter)
        {
            // Вызываем событие только когда пользователь нажал кнопку
            FilterApplied?.Invoke(this, EventArgs.Empty);
        }

        private void ClearFilter(object parameter)
        {
            NameFilter = string.Empty;
            MinAge = null;
            MaxAge = null;
            MinSalary = null;
            MaxSalary = null;
            Specialization = string.Empty;

            // Вызываем отдельное событие для очистки
            FilterCleared?.Invoke(this, EventArgs.Empty);
        }

        private void Close(object parameter)
        {
            // Просто закрываем - без применения фильтра
        }

        public FilterCriteriaDTO ToFilterCriteria()
        {
            return new FilterCriteriaDTO
            {
                Name = NameFilter,
                MinAge = MinAge,
                MaxAge = MaxAge,
                MinSalary = MinSalary,
                MaxSalary = MaxSalary,
                Specialization = Specialization
            };
        }

        // Метод для проверки, применен ли какой-либо фильтр
        public bool HasActiveFilters()
        {
            return !string.IsNullOrEmpty(NameFilter) ||
                   MinAge.HasValue ||
                   MaxAge.HasValue ||
                   MinSalary.HasValue ||
                   MaxSalary.HasValue ||
                   !string.IsNullOrEmpty(Specialization);
        }
    }
}