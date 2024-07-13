using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFactory.Utilities;
using System.Windows.Input;

namespace SmartFactory.ViewModels
{
    internal class NavigationVM : ViewModelBase
    {
        private object _viewModel;
        public object viewModel 
        { 
            get { return _viewModel; } 
            set { _viewModel = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand GraphCommand { get; set;}
        public ICommand ManagementCommand { get; set; }

    }
}
