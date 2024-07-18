using LiveChartsCore.SkiaSharpView.Painting.ImageFilters;
using SmartFactory.Helpers;
using SmartFactory.Models;
using SmartFactory.Views;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SmartFactory
{
    public partial class Signup : Window
    {
        private string employeeId;
        private Management _management; // 필드 추가

        // Management 인스턴스를 인자로 받는 생성자
        public Signup(Management management = null)
        {
            InitializeComponent();
            _management = management;
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
            if (!string.IsNullOrEmpty(Common.LogginedId) && Common.IsLogined == true)
            {
                this.Close();
            }
            else
            {
                this.Close();
                Login login = new Login();
                login.Topmost = true;
                login.ShowDialog();
            }
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

            if (string.IsNullOrEmpty(TxtGender.Text)) // 아이디를 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "성별을 입력하세요\n";
            }

            if (string.IsNullOrEmpty(TxtEmail.Text)) // 비밀번호를 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "이메일을 입력하세요\n";
            }

            if (string.IsNullOrEmpty(TxtPhone.Text)) // 전화번호를 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "전화번호를 입력하세요\n";
            }

            if (string.IsNullOrEmpty(TxtAddress.Text)) // 성별을 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "주소를 입력하세요\n";
            }
            if (string.IsNullOrEmpty(TxtPosition.Text)) // 성별을 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "직급을 입력하세요\n";
            }
            if (string.IsNullOrEmpty(TxtLoginId.Text)) // 성별을 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "아이디를 입력하세요\n";
            }
            if (string.IsNullOrEmpty(TxtLoginPwd.Text)) // 성별을 입력하지 않았을 때
            {
                isFail = true;
                errMsg += "비밀번호를 입력하세요\n";
            }

            if (isFail == true)
            {
                MessageBox.Show("(필수)\n" + errMsg, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SignUpProcess())
            {
                this.Close(); // 회원정보를 문제없이 입력했으면 회원가입창을 닫는다
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

            if (!string.IsNullOrEmpty(Common.LogginedId) && Common.IsLogined == true)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(Models.Employees.EMPLOYEE_UPDATE_QUERY, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@gender", gender);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@phone", phone);
                            cmd.Parameters.AddWithValue("@address", address);
                            cmd.Parameters.AddWithValue("@position", position);
                            cmd.Parameters.AddWithValue("@LoginId", LoginId);
                            cmd.Parameters.AddWithValue("@LoginPw", LoginPw);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("직원이 성공적으로 수정되었습니다.");
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("수정할 직원을 찾을 수 없습니다.");
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터 수정 중 오류가 발생했습니다: " + ex.Message);
                    return false;
                }
            }
            else
            {
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
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@gender", gender);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@phone", phone);
                            cmd.Parameters.AddWithValue("@address", address);
                            cmd.Parameters.AddWithValue("@position", position);
                            cmd.Parameters.AddWithValue("@LoginIdx", LoginIdx);
                            cmd.Parameters.AddWithValue("@LoginId", LoginId);
                            cmd.Parameters.AddWithValue("@LoginPw", LoginPw);

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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtLoginId.Focus();

            TxtName.Text = string.Empty;
            TxtGender.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtPhone.Text = string.Empty;
            TxtAddress.Text = string.Empty;
            TxtPosition.Text = string.Empty;
            TxtLoginId.Text = string.Empty;
            TxtLoginPwd.Text = string.Empty;

            if (!string.IsNullOrEmpty(Common.LogginedId) && Common.IsLogined == true)
            {
                // 아이디로 SQL 쿼리로 데이터 가져옴
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    var selQuery = Models.Employees.EMPLOYEE_SELECT_DETAIL_QUERY;
                    using (SqlCommand cmd = new SqlCommand(selQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", Common.LogginedId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TxtName.Text = reader["name"].ToString();
                                TxtGender.Text = reader["gender"].ToString();
                                TxtEmail.Text = reader["email"].ToString();
                                TxtPhone.Text = reader["phone"].ToString();
                                TxtAddress.Text = reader["address"].ToString();
                                TxtPosition.Text = reader["position"].ToString();
                                TxtLoginId.Text = reader["LoginId"].ToString();
                                TxtLoginPwd.Text = reader["LoginPw"].ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}


