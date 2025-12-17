using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presenter.ViewModels.Base;

namespace Presenter
{
    public class ViewModelEventArgs : EventArgs
    {
        public ViewModelBase ViewModel { get; }

        public ViewModelEventArgs(ViewModelBase viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
