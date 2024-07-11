using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace teamproject4
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

        /* 처음만 Login
        // Login 클릭
        private void TxtLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            membership Membership = new membership();
            Membership.Topmost = true;
            Membership.ShowDialog();
        }
        */

        // Logout 클릭
        private void TxtLogout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var res = MessageBox.Show("로그아웃 하시겠습니까?", "로그아웃창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                this.Hide();
                homepage Homepage = new homepage();
                Homepage.ShowDialog();
            }
        }

        // On 클릭
        private void TxtOn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        // Off 클릭
        private void TxtOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        // Home 버튼 클릭
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            homepage Homepage = new homepage();
            Homepage.ShowDialog();
        }

        // Management 버튼 클릭
        private void BtnManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Topmost = true;
            mainwindow.ShowDialog();
        }

        // 프로그램 종료 버튼
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("종료하시겠습니까?", "종료창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) Environment.Exit(0);
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            //Create DataGrid Items Info
            members.Add(new Member { Number = "1", Character = "j", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "드록바", Position = "coach", Email = "드록바@gmail.com", Phone = "010-1234-5678" });
            members.Add(new Member { Number = "2", Character = "r", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "윌리안", Position = "coach", Email = "윌리안@gmail.com", Phone = "010-1111-1111" });
            members.Add(new Member { Number = "3", Character = "d", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "체흐", Position = "manager", Email = "체흐@gmail.com", Phone = "010-2222-2222" });
            members.Add(new Member { Number = "4", Character = "g", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "램파드", Position = "manager", Email = "램파드@gmail.com", Phone = "010-3333-3333" });
            members.Add(new Member { Number = "5", Character = "l", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "카르발듀", Position = "coach", Email = "카르발듀@gmail.com", Phone = "010-4444-4444" });
            members.Add(new Member { Number = "6", Character = "b", BgColor = (Brush)converter.ConvertFromString("#6741d9"), Name = "에메르송", Position = "coach", Email = "에메르송@gmail.com", Phone = "010-5555-5555" });
            members.Add(new Member { Number = "7", Character = "s", BgColor = (Brush)converter.ConvertFromString("#ff6d00"), Name = "마운트", Position = "administrator", Email = "마운트@gmail.com", Phone = "010-6666-6666" });
            members.Add(new Member { Number = "8", Character = "a", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "엔소", Position = "manager", Email = "엔소@gmail.com", Phone = "010-7777-7777" });
            members.Add(new Member { Number = "9", Character = "f", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "조콜", Position = "manager", Email = "조콜@gmail.com", Phone = "010-8888-8888" });
            members.Add(new Member { Number = "10", Character = "s", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "페드로", Position = "administrator", Email = "페드로@gmail.com", Phone = "010-9999-9999" });

            members.Add(new Member { Number = "1", Character = "j", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "드록바", Position = "coach", Email = "드록바@gmail.com", Phone = "010-1234-5678" });
            members.Add(new Member { Number = "2", Character = "r", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "윌리안", Position = "coach", Email = "윌리안@gmail.com", Phone = "010-1111-1111" });
            members.Add(new Member { Number = "3", Character = "d", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "체흐", Position = "manager", Email = "체흐@gmail.com", Phone = "010-2222-2222" });
            members.Add(new Member { Number = "4", Character = "g", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "램파드", Position = "manager", Email = "램파드@gmail.com", Phone = "010-3333-3333" });
            members.Add(new Member { Number = "5", Character = "l", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "카르발듀", Position = "coach", Email = "카르발듀@gmail.com", Phone = "010-4444-4444" });
            members.Add(new Member { Number = "6", Character = "b", BgColor = (Brush)converter.ConvertFromString("#6741d9"), Name = "에메르송", Position = "coach", Email = "에메르송@gmail.com", Phone = "010-5555-5555" });
            members.Add(new Member { Number = "7", Character = "s", BgColor = (Brush)converter.ConvertFromString("#ff6d00"), Name = "마운트", Position = "administrator", Email = "마운트@gmail.com", Phone = "010-6666-6666" });
            members.Add(new Member { Number = "8", Character = "a", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "엔소", Position = "manager", Email = "엔소@gmail.com", Phone = "010-7777-7777" });
            members.Add(new Member { Number = "9", Character = "f", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "조콜", Position = "manager", Email = "조콜@gmail.com", Phone = "010-8888-8888" });
            members.Add(new Member { Number = "10", Character = "s", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "페드로", Position = "administrator", Email = "페드로@gmail.com", Phone = "010-9999-9999" });

            members.Add(new Member { Number = "1", Character = "j", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "드록바", Position = "coach", Email = "드록바@gmail.com", Phone = "010-1234-5678" });
            members.Add(new Member { Number = "2", Character = "r", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "윌리안", Position = "coach", Email = "윌리안@gmail.com", Phone = "010-1111-1111" });
            members.Add(new Member { Number = "3", Character = "d", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "체흐", Position = "manager", Email = "체흐@gmail.com", Phone = "010-2222-2222" });
            members.Add(new Member { Number = "4", Character = "g", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "램파드", Position = "manager", Email = "램파드@gmail.com", Phone = "010-3333-3333" });
            members.Add(new Member { Number = "5", Character = "l", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "카르발듀", Position = "coach", Email = "카르발듀@gmail.com", Phone = "010-4444-4444" });
            members.Add(new Member { Number = "6", Character = "b", BgColor = (Brush)converter.ConvertFromString("#6741d9"), Name = "에메르송", Position = "coach", Email = "에메르송@gmail.com", Phone = "010-5555-5555" });
            members.Add(new Member { Number = "7", Character = "s", BgColor = (Brush)converter.ConvertFromString("#ff6d00"), Name = "마운트", Position = "administrator", Email = "마운트@gmail.com", Phone = "010-6666-6666" });
            members.Add(new Member { Number = "8", Character = "a", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "엔소", Position = "manager", Email = "엔소@gmail.com", Phone = "010-7777-7777" });
            members.Add(new Member { Number = "9", Character = "f", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "조콜", Position = "manager", Email = "조콜@gmail.com", Phone = "010-8888-8888" });
            members.Add(new Member { Number = "10", Character = "s", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "페드로", Position = "administrator", Email = "페드로@gmail.com", Phone = "010-9999-9999" });

            //membersDataGrid.ItemSource = members;\
            membersDataGrid.ItemsSource = members;
        }

        private void BtnHome_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Member
    {
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Brush BgColor
        {
            get; set;
        }
    }
}
