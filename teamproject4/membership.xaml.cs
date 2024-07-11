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
    /// membership.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class membership : Window
    {
        public membership()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // 취소 버튼 클릭
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 저장 버튼 클릭
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
