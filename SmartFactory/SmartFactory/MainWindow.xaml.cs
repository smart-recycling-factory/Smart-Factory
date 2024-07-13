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
                BtnOn.IsEnabled = true;
                BtnOff.IsEnabled = true;

                BtnHome.IsEnabled = true;
                BtnGraph.IsEnabled = true;
                BtnManagement.IsEnabled = true;
            }
            else
            {
                BtnLogin.IsEnabled = true;
                BtnLogout.IsEnabled = false;
                BtnOn.IsEnabled = false;
                BtnOff.IsEnabled = false;

                BtnHome.IsEnabled = false;
                BtnGraph.IsEnabled = false;
                BtnManagement.IsEnabled = false;
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
            var res = MessageBox.Show("로그아웃 하시겠습니까?", "로그아웃창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                BtnLogin.IsEnabled = true;
                BtnLogout.IsEnabled = false;
                BtnOn.IsEnabled = false;
                BtnOff.IsEnabled = false;

                this.Hide();

                Login login = new Login();
                login.ShowDialog();

                if (login.DialogResult == true)
                {
                    // 로그인 성공 시 다시 메인 창
                    this.Show();
                }
                else
                {
                    // 로그인 창에서 취소됐을 경우 종료 처리 등
                    Application.Current.Shutdown();
                }
            }
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