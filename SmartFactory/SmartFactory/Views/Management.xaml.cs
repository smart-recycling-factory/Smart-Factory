using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using SmartFactory.Helpers;
using SmartFactory.Models;
using SmartFactory.ViewModels;

namespace SmartFactory.Views
{
    public partial class Management : UserControl
    {
        public ManagementVM ViewModel { get; private set; }
        public ObservableCollection<Employees> Employees { get; set; }
        private Employees selectedEmployee; // 직원 선택 변수

        public Management()
        {
            InitializeComponent();
            this.Loaded += Management_Loaded;
            ViewModel = new ManagementVM();
            DataContext = ViewModel;
            Employees = new ObservableCollection<Employees>();
            membersDataGrid.ItemsSource = Employees;

            ViewModel.get_data_graph();
        }

        // 창 호출 애니메이션
        private void Management_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.8)
            };

            this.BeginAnimation(UserControl.OpacityProperty, fadeInAnimation);
        }

        private bool IsMaximized = false;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(Models.Employees.EMPLOYEE_SELECT_QUERY, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dSet = new DataSet();
                adapter.Fill(dSet, "Employees");

                Employees.Clear();

                foreach (DataRow row in dSet.Tables["Employees"].Rows)
                {
                    Employees.Add(new Employees
                    {
                        EmployeeId = Convert.ToInt32(row["employeeId"]),
                        Name = Convert.ToString(row["name"]),
                        Gender = Convert.ToString(row["gender"]),
                        Position = Convert.ToString(row["position"]),
                        Email = Convert.ToString(row["email"]),
                        Phone = Convert.ToInt32(row["phone"]),
                        Address = Convert.ToString(row["address"]),
                    });
                }
            }
        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = (Employees)membersDataGrid.SelectedItem; // DataGrid에서 선택한 항목을 selectedEmployee에 저장
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee != null)
            {
                int employeeId = selectedEmployee.EmployeeId; // 선택된 직원의 ID 가져오기

                Signup signup = new Signup(this, employeeId); // 관리 인스턴스와 직원 ID를 넘기면서 Signup 창을 호출합니다.
                signup.ShowDialog();
            }
            else
            {
                MessageBox.Show("수정할 직원을 선택하세요.");
            }
        }


        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (membersDataGrid.SelectedItem is Employees selectedEmployee)
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(Models.Employees.EMPLOYEE_DELETE_QUERY, conn))
                        {
                            cmd.Parameters.AddWithValue("@employeeId", selectedEmployee.EmployeeId);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("직원이 성공적으로 삭제되었습니다.");
                                Employees.Remove(selectedEmployee);
                            }
                            else
                            {
                                MessageBox.Show("삭제할 직원을 찾을 수 없습니다.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("데이터 삭제 중 오류가 발생했습니다: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("삭제할 직원을 선택하세요.");
            }
        }
    }
}

