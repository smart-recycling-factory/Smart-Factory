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

namespace SmartFactory.Views
{
    public partial class Management : UserControl
    {
        public ViewModel1 ViewModel1 { get; private set; }
        public ObservableCollection<Employees> Employees { get; set; }
        private Employees selectedEmployee; // 직원 선택 변수

        public Management()
        {
            InitializeComponent();
            this.Loaded += Management_Loaded;
            ViewModel1 = new ViewModel1();
            DataContext = ViewModel1;
            Employees = new ObservableCollection<Employees>();
            membersDataGrid.ItemsSource = Employees;

            ViewModel1.get_data_graph();
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
    public class ViewModel1
    {
        public int red { get; private set; }
        public int green { get; private set; }
        public int blue { get; private set; }
        public int redToday { get; private set; }
        public int greenToday { get; private set; }
        public int blueToday { get; private set; }

        public ISeries[] PieSeries { get; private set; }
        public ISeries[] ColumnSeries { get; private set; }
        public Axis[] XAxes { get; private set; }

        public ViewModel1()
        {
            PieSeries = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } }
            };

            ColumnSeries = new ISeries[]
            {
                new ColumnSeries<double> { Name = "Category", Values = new double[] { 0 } },
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = new string[] { "" },
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    SeparatorsAtCenter = false,
                    TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                    TicksAtCenter = true,
                    ForceStepToMin = true,
                    MinStep = 1
                }
            };

        }

        public void get_data_graph()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    using (var Pcount = new SqlCommand(Models.Result.RESULT_SELECT_PLASTIC_SUM, conn))
                    {
                        var P_result = Pcount.ExecuteScalar();
                        red = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Models.Result.RESULT_SELECT_PAPER_SUM, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        green = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Models.Result.RESULT_SELECT_CAN_SUM, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        blue = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }

                    using (var Pcount = new SqlCommand(Models.Result.RESULT_SELECT_PLASTIC, conn))
                    {
                        var P_result = Pcount.ExecuteScalar();
                        redToday = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Models.Result.RESULT_SELECT_PAPER, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        greenToday = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Models.Result.RESULT_SELECT_CAN, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        blueToday = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }


                    // PieSeries 업데이트
                    PieSeries = new ISeries[]
                    {
                        new PieSeries<double>
                        {
                            Name = "PLASTIC",
                            Values = new double[] { red != 0 ? red : double.NaN },
                            Fill = new SolidColorPaint(SKColors.Blue)
                        },
                        new PieSeries<double>
                        {
                            Name = "PAPER",
                            Values = new double[] { green != 0 ? green : double.NaN },
                            Fill = new SolidColorPaint(SKColors.Red)
                        },
                        new PieSeries<double>
                        {
                            Name = "CAN",
                            Values = new double[] { blue != 0 ? blue : double.NaN },
                            Fill = new SolidColorPaint(SKColors.Green)
                        }
                    };

                    // ColumnSeries 업데이트
                    ColumnSeries = new ISeries[]
                    {
                        new ColumnSeries<double>
                        {
                            Name = "Plastic",
                            Values = new double[] { redToday }
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Paper",
                            Values = new double[] { greenToday }
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Can",
                            Values = new double[] { blueToday }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 조회 중 오류가 발생했습니다: " + ex.Message);
            }
        }
    }


}

