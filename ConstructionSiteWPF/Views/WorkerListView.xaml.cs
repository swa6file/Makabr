using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Presenter;
using Presenter.ViewModels.Worker;

namespace ConstructionSiteWPF.Views
{
    public partial class WorkerListView : Window
    {
        public WorkerListView()
        {
            InitializeComponent();
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("WorkerListView загружен!");

            if (DataContext is WorkerListViewModel viewModel)
            {
                Debug.WriteLine("DataContext установлен как WorkerListViewModel");

                // Подписываемся на событие открытия дочерних окон
                viewModel.OnChildViewModelRequested += OnChildViewModelRequested;

                Debug.WriteLine("Подписка на OnChildViewModelRequested выполнена");
            }
            else
            {
                Debug.WriteLine("DataContext не является WorkerListViewModel!");
            }
        }

        private void OnChildViewModelRequested(object sender, ViewModelEventArgs e)
        {
            Debug.WriteLine($"OnChildViewModelRequested вызван для {e.ViewModel?.GetType().Name}");

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (e.ViewModel is WorkerEditViewModel editVM)
                {
                    Debug.WriteLine("Открываем WorkerEditView");
                    var editWindow = new WorkerEditView();
                    editWindow.DataContext = editVM;
                    editWindow.Owner = this;
                    editWindow.ShowDialog();
                    Debug.WriteLine("WorkerEditView закрыт");
                }
                else if (e.ViewModel is WorkerFilterViewModel filterVM)
                {
                    Debug.WriteLine("Открываем WorkerFilterView");
                    var filterWindow = new WorkerFilterView();
                    filterWindow.DataContext = filterVM;
                    filterWindow.Owner = this;

                    // Просто открываем окно - фильтр сам обновит список через события
                    filterWindow.ShowDialog();
                    Debug.WriteLine("WorkerFilterView закрыт");
                }
            });
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}