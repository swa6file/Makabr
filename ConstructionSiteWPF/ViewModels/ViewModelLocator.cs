using System.ComponentModel;
using System.Windows;

namespace ConstructionSiteWPF.ViewModels
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Design-time ViewModel для MainWindow
        /// </summary>
        public Presenter.ViewModels.MainViewModel MainViewModel
        {
            get
            {
                if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                {
                    // Возвращаем тестовые данные для дизайнера
                    return new Presenter.ViewModels.MainViewModel();
                }

                // В runtime возвращаем null - ViewManager сам установит DataContext
                return null;
            }
        }

        /// <summary>
        /// Design-time ViewModel для WorkerListView
        /// </summary>
        public Presenter.ViewModels.Worker.WorkerListViewModel WorkerListViewModel
        {
            get
            {
                if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                {
                    return new Presenter.ViewModels.Worker.WorkerListViewModel();
                }

                return null;
            }
        }
    }
}