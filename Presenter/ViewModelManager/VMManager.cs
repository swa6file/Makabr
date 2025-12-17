using System;
using System.Collections.Generic;
using Presenter.ViewModels;
using Presenter.ViewModels.Base;
using Presenter.ViewModels.Worker;

namespace Presenter.ViewModelManager
{
    public class VMManager
    {
        public event EventHandler<ViewModelEventArgs> ViewModelReady;

        private readonly Dictionary<Type, ViewModelBase> _viewModels = new Dictionary<Type, ViewModelBase>();

        public T GetViewModel<T>() where T : ViewModelBase
        {
            var type = typeof(T);
            if (!_viewModels.ContainsKey(type))
            {
                var viewModel = Activator.CreateInstance<T>();
                _viewModels[type] = viewModel;
            }
            return (T)_viewModels[type];
        }

        public void InitializeMainViewModel()
        {
            var mainViewModel = GetViewModel<MainViewModel>();

            // Подписываемся на навигационные события
            mainViewModel.OnViewModelRequested += (sender, e) =>
            {
                // Загружаем данные для ViewModel если нужно
                if (e.ViewModel is WorkerListViewModel workerListVM)
                {
                    workerListVM.LoadData();
                }
                else if (e.ViewModel is ConstructionInfoViewModel constructionInfoVM)
                {
                    constructionInfoVM.LoadData();
                }

                // Пробрасываем событие о готовности ViewModel
                ViewModelReady?.Invoke(sender, e);
            };

            // Сообщаем, что главная ViewModel готова
            ViewModelReady?.Invoke(this, new ViewModelEventArgs(mainViewModel));
        }
    }
}