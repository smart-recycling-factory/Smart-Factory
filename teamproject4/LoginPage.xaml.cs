using MahApps.Metro.Controls;
using System;
using System.Data.SqlClient;
using System.Windows;
using teamproject4;
using teamproject4.Helpers;
using teamproject4.Models;

namespace Smart_LoginPage
{
    public partial class LoginPage : Window
    {
        // 데이터베이스 연결 문자열
        string CONN = "Data Source=localhost;Initial Catalog=smart_factory;Persist Security Info=True;User ID=sa;Encrypt=False;Password=mssql_p@ss";

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

        // 로그인 버튼
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(CONN))
                {
                    conn.Open();

                    string query = @"SELECT COUNT(1) 
                                     FROM [dbo].[employee] 
                                     WHERE [LoginId] = @LoginId AND [LoginPw] = @LoginPw";

                    string textboxID = textbox_ID.Text; // TextBox의 Text 속성 값을 가져옵니다.
                    string textboxPW = textbox_PW.Text; // TextBox의 Text 속성 값을 가져옵니다.

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Text 속성 값을 매개변수에 추가합니다.
                        cmd.Parameters.AddWithValue("@LoginId", textboxID);
                        cmd.Parameters.AddWithValue("@LoginPw", textboxPW);

                        int loginStatus = (int)cmd.ExecuteScalar();

                        if (loginStatus == 1)
                        {
                            Common.LogginedId = textboxID;
                            Common.IsLogined = true; // 로그인 된 상태다!

                            this.Hide();

                            homepage homepage = new homepage();
                            homepage.ShowDialog();

                            
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
    }
}
