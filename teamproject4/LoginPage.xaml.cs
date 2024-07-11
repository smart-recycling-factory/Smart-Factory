using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using teamproject4;

namespace Smart_LoginPage
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : MetroWindow
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            membership Membership = new membership();
            Membership.Topmost = true;
            Membership.ShowDialog();
        }
    }
}