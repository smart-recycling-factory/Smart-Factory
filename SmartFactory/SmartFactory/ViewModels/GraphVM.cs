using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFactory.Models;

namespace SmartFactory.ViewModels
{
    class GraphVM : Utilities.ViewModelBase
    {
        private readonly Result _result;
        public int Plastic
        {
            get { return _result.Plastic; }
            set { _result.Plastic = value; OnPropertyChanged(); }
        }
        
        public int Paper
        {
            get { return _result.Paper; }
            set { _result.Paper = value; OnPropertyChanged(); }
        }

        public int Can
        {
            get { return _result.Can; }
            set { _result.Can = value; OnPropertyChanged(); }
        }

        public GraphVM()
        {
            _result = new Result();
            Plastic = int.MaxValue;
            Paper = int.MaxValue;
            Can = int.MaxValue;
        }

    }

}
