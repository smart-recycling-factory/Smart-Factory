using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;
using teamproject4.Models;
using teamproject4.Helpers; // Ensure to add this using statement

namespace teamproject4
{
    public partial class Graphpage : Window
    {
        public Graphpage()
        {
            InitializeComponent();
            DataContext = this;
            LoadChartData();
        }

        private void LoadChartData()
        {
            var results = new List<Employees.result>();
            
            // 데이터베이스 연결 및 데이터 읽기
            using (SqlConnection connection = new SqlConnection(Common.CONNSTRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Employees.result.EMPLOYEE_SELECT_QUERY, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var result = new Employees.result
                    {
                        plastic = (int)reader["plastic"],
                        paper = (int)reader["paper"],
                        can = (int)reader["can"]
                    };
                    results.Add(result);
                }
            }

            var plasticValues = results.Select(r => r.plastic).ToArray();
            var paperValues = results.Select(r => r.paper).ToArray();
            var canValues = results.Select(r => r.can).ToArray();

            Series = new ObservableCollection<ISeries>
            {
                new ColumnSeries<int>
                {
                    Name = "Plastic",
                    Values = plasticValues,
                    Fill = new SolidColorPaint(SKColors.Blue)
                },
                new ColumnSeries<int>
                {
                    Name = "Paper",
                    Values = paperValues,
                    Fill = new SolidColorPaint(SKColors.Green)
                },
                new ColumnSeries<int>
                {
                    Name = "Can",
                    Values = canValues,
                    Fill = new SolidColorPaint(SKColors.Red)
                }
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Categories",
                    NamePaint = new SolidColorPaint { Color = SKColors.Black },
                    Labels = new[] { "Plastic", "Paper", "Can" },
                    SeparatorsPaint = new SolidColorPaint
                    {
                        Color = SKColors.Gray,
                        StrokeThickness = 2
                    },
                    MinStep = 1,
                }
            };

            YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Count",
                    MinStep = 1,
                    NamePaint = new SolidColorPaint { Color = SKColors.Black },
                    SeparatorsPaint = new SolidColorPaint
                    {
                        Color = SKColors.Gray,
                        StrokeThickness = 2
                    },
                }
            };

            Sections = new ObservableCollection<RectangularSection>
            {
                new RectangularSection
                {
                    Xi = 0,
                    Xj = 2,
                    Fill = new SolidColorPaint(new SKColor(255, 0, 0, 50))
                }
            };
        }

        public ObservableCollection<ISeries> Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }
        public ObservableCollection<RectangularSection> Sections { get; set; }

        private void Pixelchart_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        // Logout 클릭
        private void TxtLogout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var res = MessageBox.Show("로그아웃 하시겠습니까?", "로그아웃창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                this.Hide();
                firstpage Firstpage = new firstpage();
                Firstpage.ShowDialog();
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

        // Graph 버튼 클릭
        private void BtnGraph_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Graphpage graphpage = new Graphpage();
            graphpage.Topmost = true;
            graphpage.ShowDialog();
        }

        // Management 버튼 클릭
        private void BtnManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Topmost = true;
            mainwindow.ShowDialog();
        }

        // 프로그램 종료버튼
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("종료하시겠습니까?", "종료창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) Environment.Exit(0);
        }
    }
}
