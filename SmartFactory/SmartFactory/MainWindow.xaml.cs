using System.Text;
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

namespace SmartFactory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(Common.LogginedId) && Common.IsLogined == true)
            {
                BtnLogin.IsEnabled = false;
                BtnLogout.IsEnabled = true;
            }
            else
            {
                BtnLogin.IsEnabled = true;
                BtnLogout.IsEnabled = false;
            }
        }

        // 로그인 버튼
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Topmost = true;
            login.ShowDialog();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOff_Click(object sender, RoutedEventArgs e)
        {

        }

        // Home 버튼
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Views.Home();
        }

        // Graph 버튼
        private void BtnGraph_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Views.Graph();
        }

        // Management 버튼
        private void BtnManagement_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Views.Management();
        }

        // 종료 버튼
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("종료하시겠습니까?", "종료창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) Environment.Exit(0);
        }
    }
}