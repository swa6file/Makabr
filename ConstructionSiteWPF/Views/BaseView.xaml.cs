using System.Windows;

namespace ConstructionSiteWPF.Views
{
    /// <summary>
    /// Базовый класс для всех View в приложении
    /// </summary>
    public partial class BaseView : Window  
    {
        public BaseView()
        {
            InitializeComponent(); 

            // Устанавливаем стили и настройки для всех окон
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.CanResize;
        }
    }
}