using System.IO.Ports;
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
using Microsoft.Data.SqlClient;
using SmartFactory.Helpers;
using SmartFactory.Models;

namespace SmartFactory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 시리얼 포트 초기화
        SerialPort port = new SerialPort("COM6", 9600);
        // 수신된 시리얼 모니터 출력 데이터 누적
        private StringBuilder received_Data = new StringBuilder();
        private int pst;
        private int can;
        private int box;

        public MainWindow()
        {
            InitializeComponent();
            // 시리얼 포트 설정

            port.DataReceived += Port_DataReceived;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(Common.LoginedId) && Common.IsLogined == true)
            {
                BtnLogin.IsEnabled = false;
                BtnLogout.IsEnabled = true;
                BtnOn.IsEnabled = true;
                BtnOff.IsEnabled = true;

                BtnHome.IsEnabled = true;
                BtnGraph.IsEnabled = true;
                BtnManagement.IsEnabled = Common.LoginIdx == 0;

                
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

        // 로그아웃 버튼
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
                    //Application.Current.Shutdown();
                }
            }
        }

        // On 버튼
        private void BtnOn_Click(object sender, RoutedEventArgs e)
        {
            if (!port.IsOpen)
            {
                port.Open();
            }
            port.WriteLine("go");
        }

        // Off 버튼
        private void BtnOff_Click(object sender, RoutedEventArgs e)
        {
            if (port.IsOpen)
            {
                port.WriteLine("stop");
                port.Close();
            }
        }

        // 아두이노로부터 데이터 수신했을 때 호출되는 이벤트 핸들러
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // 출력되는 데이터 읽기
                string data = port.ReadExisting();
                // StringBuilder인 received_Data에 읽어온 데이터 추가
                // 여러 줄의 데이터가 도착할 수 있으므로 모든 데이터를 하나의 문자열로 처리!
                received_Data.Append(data);

                // UI 스레드에서 데이터 처리 위해 Dispatcher.Invoke() 호출
                Dispatcher.Invoke(() =>
                {
                    // received_Data를 줄 단위로 분할, 각 줄을 '\r', '\n'로 구분하고, 빈 줄은 제거
                    string[] lines = received_Data.ToString().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        // 각 줄을 '-', ' ' 로 분할
                        string[] result = line.Split(new[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (result.Length == 6)
                        {
                            // 수신된 데이터에 따라 변수 업데이트
                            for (int i = 0; i < result.Length; i += 2)
                            {
                                string id = result[i];
                                if (int.TryParse(result[i + 1], out int count))
                                {
                                    switch (id)
                                    {
                                        case "pst":
                                            pst = count;
                                            break;
                                        case "can":
                                            can = count;
                                            break;
                                        case "box":
                                            box = count;
                                            break;
                                    }
                                }
                            }

                            // 데이터베이스에 저장
                            SaveResultToDatabase(pst, can, box);
                        }
                    }

                    // 모든 라인 처리가 끝난 후 StringBuilder 초기화
                    received_Data.Clear();
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("데이터 수신 중 오류 발생! " + ex.Message);
                });
            }
        }
        
        private void SaveResultToDatabase(int plastic, int can, int paper)
        {
            // 분류 결과가 DB에 저장될 때마다 date값 저장
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            // Result 클래스 객체 생성 후 매개변수로 전달된 값들로 초기화하여 리스트에 추가
            var recycleResult = new List<Result>
            {
                new Result { Plastic = plastic, Can = can, Paper = paper }
            };

            var insRes = 0;
            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();
                foreach (Result item in recycleResult)
                {
                    using (SqlCommand cmd = new SqlCommand(Models.Result.RESULT_INSERT_QUERY, conn))
                    {
                        cmd.Parameters.AddWithValue("@plastic", item.Plastic);
                        cmd.Parameters.AddWithValue("@paper", item.Paper);
                        cmd.Parameters.AddWithValue("@can", item.Can);
                        cmd.Parameters.AddWithValue("@date", Date);

                        insRes += cmd.ExecuteNonQuery();  // 데이터 하나마다 INSERT 쿼리 실행
                    }
                }
            }
        }

        // Home 버튼
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainImg.Opacity = 0;
            ContentArea.Content = new Views.Home();
        }

        // Graph 버튼
        private void BtnGraph_Click(object sender, RoutedEventArgs e)
        {
            MainImg.Opacity = 0;
            ContentArea.Content = new Views.Graph();
        }

        // Management 버튼
        private void BtnManagement_Click(object sender, RoutedEventArgs e)
        {
            MainImg.Opacity = 0;
            ContentArea.Content = new Views.Management();
        }

        // 종료 버튼
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("종료하시겠습니까?", "종료창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                if (port.IsOpen)
                {
                    port.WriteLine("stop");
                    port.Close();
                }
                Environment.Exit(0);
            }
        }
    }
}