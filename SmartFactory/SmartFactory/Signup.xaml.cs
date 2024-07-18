using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SmartFactory
{
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
        }

        // 창 이동 이벤트
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
            Login login = new Login();
            login.Topmost = true;
            login.ShowDialog();
        }

        // 저장 버튼 클릭
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isFail = false;
            string errMsg = string.Empty;
            if (string.IsNullOrEmpty(TxtName.Text)) // 이름을 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "이름을 입력하세요\n";
            }

            if (string.IsNullOrEmpty(TxtGender.Text)) // 성별을 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "성별을 입력하세요\n";
            }

            if (string.IsNullOrEmpty(TxtEmail.Text)) // 이메일을 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "이메일을 입력하세요\n";
            }

            if (string.IsNullOrEmpty(TxtPhone.Text)) // 전화번호를 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "전화번호를 입력하세요\n";
            }

            if (string.IsNullOrEmpty(TxtAddress.Text)) // 주소를 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "주소를 입력하세요\n";
            }
            if (string.IsNullOrEmpty(TxtPosition.Text)) // 직급을 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "직급을 입력하세요\n";
            }
            if (string.IsNullOrEmpty(TxtLoginId.Text)) // 아이디를 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "아이디를 입력하세요\n";
            }
            if (string.IsNullOrEmpty(TxtLoginPwd.Text)) // 비밀번호를 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "비밀번호를 입력하세요\n";
            }

            if (isFail == true)
            {
                MessageBox.Show("(필수)\n" + errMsg, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (isFail == true)
            {
                MessageBox.Show("(필수)\n" + errMsg, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SignUpProcess())
            {
                this.Close(); // 회원정보를 문제없이 입력했으면 회원가입창을 닫음
                Login login = new Login();
                login.ShowDialog();
            }
        }

        private bool SignUpProcess()
        {
            string name = TxtName.Text;
            string gender = TxtGender.Text;
            string email = TxtEmail.Text;
            string phone = TxtPhone.Text;
            string address = TxtAddress.Text;
            string position = TxtPosition.Text;
            string LoginId = TxtLoginId.Text;
            string LoginPw = TxtLoginPwd.Text;
            int LoginIdx = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    string checkQuery = @"SELECT COUNT(*) 
                                    FROM employee
                                    WHERE LoginId = @LoginId";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@LoginId", LoginId);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("이미 존재하는 아이디입니다.\n다른 아이디를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    string query = @"INSERT INTO [dbo].[employee]
                                            ([name]
                                            ,[gender]
                                            ,[email]
                                            ,[phone]
                                            ,[address]
                                            ,[position]
                                            ,[LoginIdx]
                                            ,[LoginId]
                                            ,[LoginPw])
                                        VALUES
                                            (@name
                                            ,@gender
                                            ,@email
                                            ,@phone
                                            ,@address
                                            ,@position
                                            ,@LoginIdx
                                            ,@LoginId
                                            ,@LoginPw)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlParameter prmName = new SqlParameter("@name", name);
                        SqlParameter prmGender = new SqlParameter("@gender", gender);
                        SqlParameter prmEmail = new SqlParameter("@email", email);
                        SqlParameter prmPhone = new SqlParameter("@phone", phone);
                        SqlParameter prmAddress = new SqlParameter("@address", address);
                        SqlParameter prmPosition = new SqlParameter("@position", position);
                        SqlParameter prmLoginIdx = new SqlParameter("@LoginIdx", LoginIdx);

                        SqlParameter prmLoginId = new SqlParameter("@LoginId", LoginId);
                        SqlParameter prmLoginPw = new SqlParameter("@LoginPw", LoginPw);
                        cmd.Parameters.Add(prmName);
                        cmd.Parameters.Add(prmGender);
                        cmd.Parameters.Add(prmEmail);
                        cmd.Parameters.Add(prmPhone);
                        cmd.Parameters.Add(prmAddress);
                        cmd.Parameters.Add(prmPosition);
                        cmd.Parameters.Add(prmLoginIdx);
                        cmd.Parameters.Add(prmLoginId);
                        cmd.Parameters.Add(prmLoginPw);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("회원가입이 완료되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("회원가입에 실패했습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // 창이 열렸을 때 텍스트박스로 포커스
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtLoginId.Focus();
        }
    }
}
