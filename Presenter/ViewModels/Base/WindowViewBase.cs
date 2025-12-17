using System.Windows;

namespace Presenter.ViewModels.Base
{
    /// <summary>
    /// Базовый класс для всех WPF окон
    /// </summary>
    public abstract class WindowViewBase : ViewBase
    {
        /// <summary>
        /// WPF окно
        /// </summary>
        protected Window Window { get; set; }

        public override void Show()
        {
            if (Window != null)
            {
                Window.DataContext = ViewModel;
                Window.Show();
            }
        }

        public override void Close()
        {
            Window?.Close();
        }
    }
}