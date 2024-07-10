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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainwindow = new MainWindow();
            mainwindow.Owner = this;
            mainwindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            mainwindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var Firstpage = new firstpage();
            Firstpage.Owner = this;
            Firstpage.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Firstpage.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbresult = MessageBox.Show("종료 창", "종료하시겠습니까?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == mbresult)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) { }
        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e) { }
        private void TextBlock_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e) { }
        private void TextBlock_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e) { }

        public ObservableCollection<ISeries> Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }
        public ObservableCollection<RectangularSection> Sections { get; set; }

        private void Pixelchart_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
