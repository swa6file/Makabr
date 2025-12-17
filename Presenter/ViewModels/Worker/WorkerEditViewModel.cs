using System;
using System.Windows;
using System.Windows.Input;
using BusinessLogical.Interfaces;
using BusinessLogical.Models;
using Presenter.DTOs;
using Presenter.ViewModels.Base;

namespace Presenter.ViewModels.Worker
{
    public class WorkerEditViewModel : ViewModelBase
    {
        private readonly ILogic _logic;
        private WorkerDTO _worker;
        private bool _isNew;

        public WorkerDTO Worker
        {
            get => _worker;
            set => SetField(ref _worker, value);
        }

        public string Title => _isNew ? "Добавление работника" : "Редактирование работника";

        // Просто enum значения как строки
        public string[] Specializations => new string[]
        {
            "Electrician",
            "Painter",
            "CraneOperator",
            "GeneralWorker"
        };

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler WorkerSaved;
        public event EventHandler CancelRequested;

        public WorkerEditViewModel(WorkerDTO worker, ILogic logic)
        {
            _logic = logic;
            _worker = worker ?? new WorkerDTO();
            _isNew = (worker == null);

            SaveCommand = new RelayCommand(SaveWorker);
            CancelCommand = new RelayCommand(Cancel);
        }

        public WorkerEditViewModel(ILogic logic) : this(null, logic)
        {
        }

        private void SaveWorker(object parameter)
        {
            try
            {
                // Валидация
                if (string.IsNullOrEmpty(Worker.Name))
                {
                    MessageBox.Show("Введите имя работника", "Ошибка",
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Worker.Age <= 0 || Worker.Age > 150)
                {
                    MessageBox.Show("Введите корректный возраст (1-150)", "Ошибка",
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Worker.Salary <= 0)
                {
                    MessageBox.Show("Введите корректную зарплату", "Ошибка",
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(Worker.Specialization))
                {
                    MessageBox.Show("Выберите специализацию", "Ошибка",
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Вызов бизнес-логики
                if (_isNew)
                {
                    var result = _logic.AddWorker(
                        Worker.Name,
                        Worker.Age,
                        Worker.Salary,
                        Worker.Specialization);

                    HandleResult(result, "добавления");
                }
                else
                {
                    var result = _logic.UpdateWorker(
                        Worker.Id,
                        Worker.Name,
                        Worker.Age,
                        Worker.Salary,
                        Worker.Specialization);

                    HandleResult(result, "редактирования");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HandleResult(Result result, string operation)
        {
            if (result.IsSuccess)
            {
                MessageBox.Show($"Работник успешно {(operation == "добавления" ? "добавлен" : "отредактирован")}!",
                               "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                WorkerSaved?.Invoke(this, EventArgs.Empty);
                CancelRequested?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show($"Ошибка {operation} работника: {result}",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object parameter)
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}