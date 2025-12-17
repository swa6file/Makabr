using System;
using System.Windows;
using Presenter.ViewModels.Worker;

namespace ConstructionSiteWPF.Views
{
    public partial class WorkerEditView : Window
    {
        public WorkerEditView()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (DataContext is WorkerEditViewModel vm)
            {
                // Подписываемся на события ViewModel только после полной загрузки окна
                vm.WorkerSaved += OnWorkerSaved;
                vm.CancelRequested += OnCancelRequested;
            }
        }

        private void OnWorkerSaved(object sender, EventArgs e)
        {
            // Просто закрываем окно - результат сохранения будет обработан в ViewModel
            this.Close();
        }

        private void OnCancelRequested(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}