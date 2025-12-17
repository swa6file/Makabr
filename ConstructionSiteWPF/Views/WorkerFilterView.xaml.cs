using System;
using System.Windows;
using Presenter.ViewModels.Worker;

namespace ConstructionSiteWPF.Views
{
    public partial class WorkerFilterView : Window
    {
        private bool _filterApplied = false;

        public WorkerFilterView()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (DataContext is WorkerFilterViewModel vm)
            {
                // Подписываемся на события ViewModel
                vm.FilterApplied += OnFilterApplied;
                vm.FilterCleared += OnFilterCleared;
            }
        }

        private void OnFilterApplied(object sender, EventArgs e)
        {
            _filterApplied = true;
            this.DialogResult = true;
            this.Close();
        }

        private void OnFilterCleared(object sender, EventArgs e)
        {
            _filterApplied = true;
            this.DialogResult = true;
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}