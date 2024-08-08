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
        private Management _management; // 필드 추가
        private int _employeeId;

        // Management 인스턴스를 인자로 받는 생성자
        public Signup(Management management, int employeeId = 0)
        {
            InitializeComponent();
            _management = management;
            _employeeId = employeeId;

            if (_employeeId > 0)
            {
                LoadEmployeeData();
                SetLoginIdReadOnly(true); // 수정할 때 LoginId를 읽기 전용으로 설정
            }
        }

        // 일반적인 회원가입창
        public Signup()
        {
            InitializeComponent();
        }

        private void SetLoginIdReadOnly(bool isReadOnly)
        {
            TxtLoginId.IsReadOnly = isReadOnly;
        }

        private void LoadEmployeeData()
        {
            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Models.Employees.EMPLOYEE_SELECT_DETAIL_QUERY, conn))
                    {
                        cmd.Parameters.AddWithValue("@employeeId", _employeeId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TxtName.Text = reader["name"].ToString();
                                TxtGender.Text = reader["gender"].ToString();
                                TxtPosition.Text = reader["position"].ToString();
                                TxtEmail.Text = reader["email"].ToString();
                                TxtPhone.Text = reader["phone"].ToString();
                                TxtAddress.Text = reader["address"].ToString();
                                TxtLoginId.Text = reader["LoginId"].ToString();
                                TxtLoginPwd.Text = reader["LoginPw"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("직원 데이터를 로드하는 동안 오류가 발생했습니다: " + ex.Message);
                }
            }
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
            if (!string.IsNullOrEmpty(Common.LoginedId) && Common.IsLogined == true)
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
            var errorMsg = ValidateInputs();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                MessageBox.Show("(필수)\n" + errorMsg, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SignUpProcess())
            {
                Close();
                if (string.IsNullOrEmpty(Common.LoginedId) || !Common.IsLogined)
                {
                    var login = new Login();
                    login.ShowDialog();
                }
            }
        }

        private string ValidateInputs()
        {
            var errMsg = string.Empty;
            if (string.IsNullOrEmpty(TxtName.Text)) errMsg += "이름을 입력하세요\n";
            if (string.IsNullOrEmpty(TxtGender.Text)) errMsg += "성별을 입력하세요\n";
            if (string.IsNullOrEmpty(TxtPhone.Text)) errMsg += "전화번호를 입력하세요\n";
            if (string.IsNullOrEmpty(TxtPosition.Text)) errMsg += "직급을 입력하세요\n";
            if (string.IsNullOrEmpty(TxtLoginId.Text)) errMsg += "아이디를 입력하세요\n";
            if (string.IsNullOrEmpty(TxtLoginPwd.Text)) errMsg += "비밀번호를 입력하세요\n";
            return errMsg;
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

            if (!string.IsNullOrEmpty(Common.LoginedId) && Common.IsLogined == true)
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
                            cmd.Parameters.AddWithValue("@employeeId", _employeeId); // employeeId는 WHERE 절에 사용

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

                        // 등록자 수
                        string countQuery = @"SELECT COUNT(*)
                                                FROM employee";

                        using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
                        {
                            int userCount = (int)countCmd.ExecuteScalar();
                            LoginIdx = (userCount == 0) ? 0 : 1;
                        }

                        // 아이디 중복여부 확인
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

            if (_employeeId > 0)
            {
                // 이름 말고 아이디로 SQL 쿼리로 데이터 가져옴
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    var selQuery = Models.Employees.EMPLOYEE_SELECT_DETAIL_QUERY;
                    using (SqlCommand cmd = new SqlCommand(selQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@employeeId", _employeeId);

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