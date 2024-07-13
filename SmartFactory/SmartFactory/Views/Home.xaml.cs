using SmartFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartFactory.Helpers;

namespace SmartFactory.Views
{
    /// <summary>
    /// Home.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (!String.IsNullOrEmpty(Common.LogginedId) && Common.IsLogined == true)
            //{
            //    BtnLogin.IsEnabled = false;
            //    BtnLogout.IsEnabled = true;
            //}
            //else
            //{
            //    BtnLogin.IsEnabled = true;
            //    BtnLogout.IsEnabled = false;
            //}
        }
    }
}

    

       

