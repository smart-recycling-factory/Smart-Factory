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
using System.Windows.Shapes;

namespace teamproject4
{
    /// <summary>
    /// Graphpage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Graphpage : Window
    {
        public Graphpage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainwindow = new MainWindow();
            mainwindow.Owner = this;
            mainwindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            mainwindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var Firstpage = new firstpage();
            Firstpage.Owner = this;
            Firstpage.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Firstpage.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbresult = MessageBox.Show("종료 창", "종료하시겠습니까?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == mbresult)
            {
                //Close();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBlock_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBlock_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
