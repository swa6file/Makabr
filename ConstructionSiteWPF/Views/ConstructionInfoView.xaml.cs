using System;
using System.Windows;
using Presenter.ViewModels;

namespace ConstructionSiteWPF.Views
{
    public partial class ConstructionInfoView : Window
    {
        public ConstructionInfoView()
        {
            InitializeComponent();
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Проверяем DataContext
            if (DataContext is ConstructionInfoViewModel viewModel)
            {
                // Убеждаемся, что данные загружены
                if (viewModel.Info == null)
                {
                    viewModel.LoadData();
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Логируем закрытие окна
            System.Diagnostics.Debug.WriteLine("ConstructionInfoView закрывается");
        }
    }
}