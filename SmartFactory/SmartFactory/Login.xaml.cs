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
using System.Data.SqlClient;
using SmartFactory;
using SmartFactory.Helpers;

namespace SmartFactory
{
    public partial class Login : Window
    {

        public Login()
        {
            InitializeComponent();
        }

        // 회원가입 버튼
        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Signup signup = new Signup();
            signup.Topmost = true;
            signup.ShowDialog();
        }

        // 로그인 버튼
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    string query = @"SELECT COUNT(1) 
                                       FROM [dbo].[employee] 
                                      WHERE [LoginId] = @LoginId 
                                        AND [LoginPw] = @LoginPw";

                    string LoginId = TxtId.Text; // TextBox의Text 속성값
                    string LoginPw = PwdPw.Password; // PasswordBox의 Password 속성값

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Text 속성 값을 매개변수에 추가합니다.
                        cmd.Parameters.AddWithValue("@LoginId", LoginId);
                        cmd.Parameters.AddWithValue("@LoginPw", LoginPw);

                        int loginStatus = (int)cmd.ExecuteScalar();

                        if (loginStatus == 1)
                        {
                            Common.LogginedId = LoginId;
                            Common.IsLogined = true; // 로그인 된 상태

                            this.Hide();
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("아이디 또는 비밀번호가 잘못되었습니다.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 조회 중 오류가 발생했습니다: " + ex.Message);
            }
        }

        // 아이디 입력창으로 자동 포커스
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtId.Focus();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region 엔터키

        private void TxtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PwdPw.Focus();
            }
        }

        private void PwdPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnLogin.Focus();
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        // 로그인 창 닫기
    }
}
