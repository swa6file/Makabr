using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using BusinessLogical.Interfaces;
using Ninject;
using Presenter.DTOs;
using Presenter.ViewModelManager;
using Presenter.ViewModels.Base;
using System.Windows.Input;
using BusinessLogical.Models;

namespace Presenter.ViewModels.Worker
{
    public class WorkerListViewModel : ViewModelBase
    {
        private readonly ILogic _logic;
        private ObservableCollection<WorkerDTO> _workers;
        private WorkerDTO _selectedWorker;

        public ObservableCollection<WorkerDTO> Workers
        {
            get => _workers;
            set => SetField(ref _workers, value);
        }

        public WorkerDTO SelectedWorker
        {
            get => _selectedWorker;
            set => SetField(ref _selectedWorker, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand RefreshCommand { get; }

        public WorkerListViewModel()
        {
            var kernel = new StandardKernel(new BusinessLogical.SimpleConfigModule());
            _logic = kernel.Get<ILogic>();

            Workers = new ObservableCollection<WorkerDTO>();

            AddCommand = new RelayCommand(AddWorker);
            EditCommand = new RelayCommand(EditWorker, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteWorker, CanEditOrDelete);
            FilterCommand = new RelayCommand(FilterWorkers);
            RefreshCommand = new RelayCommand(Refresh);

            _logic.DataChanged += OnDataChanged;
        }

        public void LoadData()
        {
            Refresh(null);
        }

        private void Refresh(object parameter)
        {
            var modelWorkers = _logic.GetAllWorkers();
            Workers = new ObservableCollection<WorkerDTO>(
                modelWorkers.Select(WorkerDTO.FromModel));
        }

        private void AddWorker(object parameter)
        {
            Debug.WriteLine("AddCommand вызван!");

            var editViewModel = new WorkerEditViewModel(null, _logic);
            editViewModel.WorkerSaved += (s, e) =>
            {
                Debug.WriteLine("WorkerSaved событие вызвано!");
                Refresh(null);
            };
            editViewModel.CancelRequested += (s, e) =>
            {
                Debug.WriteLine("CancelRequested событие вызвано!");
            };

            OnChildViewModelRequested?.Invoke(this, new ViewModelEventArgs(editViewModel));
        }

        private void EditWorker(object parameter)
        {
            Debug.WriteLine("EditCommand вызван!");

            if (SelectedWorker == null)
            {
                MessageBox.Show("Выберите работника для редактирования");
                return;
            }

            var editViewModel = new WorkerEditViewModel(SelectedWorker, _logic);
            editViewModel.WorkerSaved += (s, e) =>
            {
                Debug.WriteLine("WorkerSaved событие вызвано!");
                Refresh(null);
            };
            editViewModel.CancelRequested += (s, e) =>
            {
                Debug.WriteLine("CancelRequested событие вызвано!");
            };

            OnChildViewModelRequested?.Invoke(this, new ViewModelEventArgs(editViewModel));
        }

        private void DeleteWorker(object parameter)
        {
            Debug.WriteLine("DeleteCommand вызван!");

            if (SelectedWorker != null)
            {
                var result = _logic.DeleteWorker(SelectedWorker.Id);
                if (result.IsSuccess)
                {
                    Workers.Remove(SelectedWorker);
                    MessageBox.Show("Работник удален успешно!", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    string errorMessage = GetErrorMessage(result);
                    MessageBox.Show($"Ошибка удаления: {errorMessage}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string GetErrorMessage(Result result)
        {
            // Тот же метод что и в WorkerEditViewModel
            var resultType = result.GetType();

            var errorMessageProp = resultType.GetProperty("ErrorMessage");
            if (errorMessageProp != null)
            {
                var value = errorMessageProp.GetValue(result);
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    return value.ToString();
            }

            var messageProp = resultType.GetProperty("Message");
            if (messageProp != null)
            {
                var value = messageProp.GetValue(result);
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    return value.ToString();
            }

            return result.ToString();
        }

        private void FilterWorkers(object parameter)
        {
            Debug.WriteLine("FilterCommand вызван!");

            var filterViewModel = new WorkerFilterViewModel(_logic);
            filterViewModel.FilterApplied += (s, e) =>
            {
                Debug.WriteLine("FilterApplied событие вызвано!");
                var criteria = filterViewModel.ToFilterCriteria();
                var modelCriteria = criteria.ToModel();
                var filtered = _logic.FilterWorkers(modelCriteria);
                Workers = new ObservableCollection<WorkerDTO>(
                    filtered.Select(WorkerDTO.FromModel));
            };

            OnChildViewModelRequested?.Invoke(this, new ViewModelEventArgs(filterViewModel));
        }

        public bool CanEditOrDelete(object parameter) => SelectedWorker != null;

        private void OnDataChanged(object sender, EventArgs e)
        {
            Refresh(null);
        }

        public event EventHandler<ViewModelEventArgs> OnChildViewModelRequested;
    }
}