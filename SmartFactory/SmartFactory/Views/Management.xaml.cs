using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace SmartFactory.Views
{
    /// <summary>
    /// Management.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Management : UserControl
    {
        public ViewModel1 ViewModel1 { get; private set; }
        public Management()
        {
            InitializeComponent();
            this.Loaded += Management_Loaded;
            ViewModel1 = new ViewModel1();
            DataContext = ViewModel1;

            ViewModel1.get_data_graph();

        }
        
        // 창 호출 애니메이션
        private void Management_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.8);

            this.BeginAnimation(UserControl.OpacityProperty, fadeInAnimation);
        }

        private bool IsMaximized = false;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            //Create DataGrid Items Info


            members.Add(new Member { Number = "1", Character = "j", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "드록바", Position = "coach", Email = "드록바@gmail.com", Phone = "010-1234-5678" });
            members.Add(new Member { Number = "2", Character = "r", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "윌리안", Position = "coach", Email = "윌리안@gmail.com", Phone = "010-1111-1111" });
            members.Add(new Member { Number = "3", Character = "d", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "체흐", Position = "manager", Email = "체흐@gmail.com", Phone = "010-2222-2222" });
            members.Add(new Member { Number = "4", Character = "g", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "램파드", Position = "manager", Email = "램파드@gmail.com", Phone = "010-3333-3333" });
            members.Add(new Member { Number = "5", Character = "l", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "카르발듀", Position = "coach", Email = "카르발듀@gmail.com", Phone = "010-4444-4444" });
            members.Add(new Member { Number = "6", Character = "b", BgColor = (Brush)converter.ConvertFromString("#6741d9"), Name = "에메르송", Position = "coach", Email = "에메르송@gmail.com", Phone = "010-5555-5555" });
            members.Add(new Member { Number = "7", Character = "s", BgColor = (Brush)converter.ConvertFromString("#ff6d00"), Name = "마운트", Position = "administrator", Email = "마운트@gmail.com", Phone = "010-6666-6666" });
            members.Add(new Member { Number = "8", Character = "a", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "엔소", Position = "manager", Email = "엔소@gmail.com", Phone = "010-7777-7777" });
            members.Add(new Member { Number = "9", Character = "f", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "조콜", Position = "manager", Email = "조콜@gmail.com", Phone = "010-8888-8888" });
            members.Add(new Member { Number = "10", Character = "s", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "페드로", Position = "administrator", Email = "페드로@gmail.com", Phone = "010-9999-9999" });

            membersDataGrid.ItemsSource = members;
        }
    }

    public class Member
    {
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Brush BgColor
        {
            get; set;
        }
    }

    public class ViewModel1
    {
        public int red { get; private set; }
        public int green { get; private set; }
        public int blue { get; private set; }

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
                new ColumnSeries<double> { Name = "Category 1", Values = new double[] { 0 } },
                new ColumnSeries<double> { Name = "Category 2", Values = new double[] { 0 } },
                new ColumnSeries<double> { Name = "Category 3", Values = new double[] { 0 } }
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = new string[] { "Category 1", "Category 2", "Category 3" },
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35))
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

                    using (var Pcount = new SqlCommand(Models.Result.RESULT_SELECT_PLASTIC, conn))
                    {
                        var P_result = Pcount.ExecuteScalar();
                        red = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Models.Result.RESULT_SELECT_PAPER, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        green = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Models.Result.RESULT_SELECT_CAN, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        blue = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }

                    // PieSeries 업데이트
                    PieSeries = new ISeries[]
                    {
                        new PieSeries<double>
                        {
                            Values = new double[] { red },
                            Fill = new SolidColorPaint(SKColors.Red)
                        },
                        new PieSeries<double>
                        {
                            Values = new double[] { green },
                            Fill = new SolidColorPaint(SKColors.Green)
                        },
                        new PieSeries<double>
                        {
                            Values = new double[] { blue },
                            Fill = new SolidColorPaint(SKColors.Blue)
                        }
                    };

                    // ColumnSeries 업데이트
                    ColumnSeries = new ISeries[]
                    {
                        new ColumnSeries<double>
                        {
                            Name = "Plastic",
                            Values = new double[] { red }
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Paper",
                            Values = new double[] { green }
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Can",
                            Values = new double[] { blue }
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
