using System;
using System.Windows;

namespace ConstructionSiteWPF
{
    public partial class App : Application
    {
        private ViewManager _viewManager;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                // Создаем и запускаем ViewManager
                _viewManager = new ViewManager();

                // ViewManager сам создаст главное окно через ViewModel
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запуска приложения: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}