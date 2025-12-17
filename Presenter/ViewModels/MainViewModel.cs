using System;
using System.Diagnostics;
using System.Windows.Input;
using BusinessLogical.Interfaces;
using Ninject;
using Presenter.ViewModelManager;
using Presenter.ViewModels.Base;
using Presenter.ViewModels.Worker;

namespace Presenter.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ILogic _logic;
        private string _statusMessage;

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetField(ref _statusMessage, value);
        }

        public ICommand ShowWorkerListCommand { get; }
        public ICommand ShowConstructionInfoCommand { get; }
        public ICommand ExitCommand { get; }

        public MainViewModel()
        {
            var kernel = new StandardKernel(new BusinessLogical.SimpleConfigModule());
            _logic = kernel.Get<ILogic>();

            ShowWorkerListCommand = new RelayCommand(ShowWorkerList);
                ShowConstructionInfoCommand = new RelayCommand(ShowConstructionInfo);
                ExitCommand = new RelayCommand(Exit);

            Debug.WriteLine("MainViewModel создан");
        }

        private void ShowWorkerList(object parameter)
        {
            Debug.WriteLine("ShowWorkerListCommand вызван");

            var workerListViewModel = new WorkerListViewModel();
            workerListViewModel.LoadData();

            var eventArgs = new ViewModelEventArgs(workerListViewModel);
            OnViewModelRequested?.Invoke(this, eventArgs);
        }

        private void ShowConstructionInfo(object parameter)
        {
            Debug.WriteLine("ShowConstructionInfoCommand вызван");

            var constructionInfoViewModel = new ConstructionInfoViewModel();
            constructionInfoViewModel.LoadData();

            var eventArgs = new ViewModelEventArgs(constructionInfoViewModel);
            OnViewModelRequested?.Invoke(this, eventArgs);
        }

        private void Exit(object parameter)
        {
            Debug.WriteLine("ExitCommand вызван");
            System.Windows.Application.Current.Shutdown();
        }

        public event EventHandler<ViewModelEventArgs> OnViewModelRequested;
    }
}