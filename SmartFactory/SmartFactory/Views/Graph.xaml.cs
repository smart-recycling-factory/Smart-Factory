using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Microsoft.Data.SqlClient;
using SkiaSharp;
using SmartFactory;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartFactory.Models;
using System.Windows.Media.Animation;

namespace SmartFactory.Views
{
    /// <summary>
    /// Graph.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Graph : UserControl
    {
        public ViewModel ViewModel { get; private set; }

        public Graph()
        {
            InitializeComponent();
            this.Loaded += Graph_Loaded;
            ViewModel = new ViewModel();
            DataContext = ViewModel;

            ViewModel.get_data_graph();
        }

        // 창 불러올때 서서히 나타나도록
        private void Graph_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(1);

            this.BeginAnimation(UserControl.OpacityProperty, fadeInAnimation);
        }
    }


    public class ViewModel
    {
        public int plastic { get; private set; }
        public int paper { get; private set; }
        public int can { get; private set; }

        public ISeries[] Series { get; private set; }


        public ViewModel()
        {
            Series = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } },
                new PieSeries<double> { Values = new double[] { 0 } }
            };
        }

        public void get_data_graph()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    using (var Pcount = new SqlCommand(Result.RESULT_SELECT_PLASTIC, conn))
                    {
                        var P_result = Pcount.ExecuteScalar();
                        plastic = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Result.RESULT_SELECT_PAPER, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        paper = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Result.RESULT_SELECT_CAN, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        can = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }

                    // Series 속성 업데이트 및 색상 설정
                    Series = new ISeries[]
                    {
                        new PieSeries<double>
                        {
                            Values = new double[] { plastic },
                            //Fill = new SolidColorPaint(SKColors.Red) // 빨간색
                            Fill = new SolidColorPaint(new SKColor(0xD0, 0xE1, 0xE9))  // #D0E1E9 색상
                        },
                        new PieSeries<double>
                        {
                            Values = new double[] { paper },
                            //Fill = new SolidColorPaint(SKColors.Green) // 초록색
                            Fill = new SolidColorPaint(new SKColor(0xA1, 0xC2, 0xD5))  // #A1C2D5 색상
                        },
                        new PieSeries<double>
                        {
                            Values = new double[] { can },
                            //Fill = new SolidColorPaint(SKColors.Blue) // 파란색
                            Fill = new SolidColorPaint(new SKColor(0xCA, 0xC6, 0xBD))  // #CAC6BD 색상
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
