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
    /// firstpage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class firstpage : Window
    {
        public firstpage()
        {
            InitializeComponent();
        }

        /* 활성화 필요없음
        // Home 버튼 클릭
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            firstpage Firstpage = new firstpage();
            Firstpage.Topmost = true;
            Firstpage.ShowDialog();
        }

        // Graph 버튼 클릭
        private void BtnGraph_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Graphpage graphpage = new Graphpage();
            graphpage.Topmost = true;
            graphpage.ShowDialog();
        }

        // Management 버튼 클릭
        private void BtnManagement_Click(object sender, RoutedEventArgs e)
        {   
            this.Hide();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Topmost = true;
            mainwindow.ShowDialog();
        }
        */

        // Login 클릭
        private void TxtLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            membership Membership = new membership();
            Membership.Topmost = true;
            Membership.ShowDialog();
        }

        // 프로그램 종료버튼
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("종료하시겠습니까?", "종료창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) Environment.Exit(0);
        }

    }
}
