using System;
using System.Collections.Generic;
using System.Windows;
using Presenter;
using Presenter.ViewModelManager;
using Presenter.ViewModels;
using Presenter.ViewModels.Base;
using Presenter.ViewModels.Worker;

namespace ConstructionSiteWPF
{
    public class ViewManager
    {
        private readonly VMManager _vmManager;
        private readonly Dictionary<Type, Type> _viewModelToViewMapping;

        public ViewManager()
        {
            _viewModelToViewMapping = new Dictionary<Type, Type>();
            InitializeMappings();

            // Создаем менеджер ViewModel
            _vmManager = new VMManager();
            _vmManager.ViewModelReady += OnViewModelReady;

            // Запускаем главную ViewModel
            _vmManager.InitializeMainViewModel();
        }

        private void InitializeMappings()
        {
            // Сопоставляем ViewModel -> View (WPF окна)
            _viewModelToViewMapping[typeof(MainViewModel)] = typeof(Views.MainWindow);
            _viewModelToViewMapping[typeof(WorkerListViewModel)] = typeof(Views.WorkerListView);
            _viewModelToViewMapping[typeof(WorkerEditViewModel)] = typeof(Views.WorkerEditView);
            _viewModelToViewMapping[typeof(ConstructionInfoViewModel)] = typeof(Views.ConstructionInfoView);
            _viewModelToViewMapping[typeof(WorkerFilterViewModel)] = typeof(Views.WorkerFilterView);
        }

        private void OnViewModelReady(object sender, ViewModelEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CreateAndShowView(e.ViewModel);
            });
        }

        private void CreateAndShowView(ViewModelBase viewModel)
        {
            var viewModelType = viewModel.GetType();

            if (_viewModelToViewMapping.TryGetValue(viewModelType, out var viewType))
            {
                // Создаем WPF окно
                var window = Activator.CreateInstance(viewType) as Window;
                if (window != null)
                {
                    // Устанавливаем DataContext
                    window.DataContext = viewModel;

                    // Настраиваем окно в зависимости от типа
                    if (window is Views.MainWindow)
                    {
                        // Главное окно
                        Application.Current.MainWindow = window;
                        window.Show();
                        System.Diagnostics.Debug.WriteLine($"MainWindow показано");
                    }
                    else if (window is Views.ConstructionInfoView)
                    {
                        // ConstructionInfoView - диалоговое окно
                        System.Diagnostics.Debug.WriteLine($"Открываем ConstructionInfoView как диалог");
                        window.Owner = Application.Current.MainWindow;
                        window.ShowDialog(); // Важно: ShowDialog() вместо Show()
                        System.Diagnostics.Debug.WriteLine($"ConstructionInfoView закрыт");
                    }
                    else if (window is Views.WorkerEditView || window is Views.WorkerFilterView)
                    {
                        // Эти окна открываются как диалоговые из других окон
                        System.Diagnostics.Debug.WriteLine($"{viewModelType.Name} должен открываться из родительского окна");
                    }
                    else
                    {
                        // Остальные окна (WorkerListView)
                        window.Owner = Application.Current.MainWindow;
                        window.Show();
                        System.Diagnostics.Debug.WriteLine($"{viewModelType.Name} показано");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Не удалось создать окно типа {viewType}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Нет маппинга для ViewModel типа {viewModelType}");
            }
        }
    }
}