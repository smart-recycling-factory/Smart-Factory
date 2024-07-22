﻿using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            LblGraphDate.Content = today;
        }

        // 창 불러올때 서서히 나타나도록
        private void Graph_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.8);

            this.BeginAnimation(UserControl.OpacityProperty, fadeInAnimation);
        }
    }


    public class ViewModel
    {
        public int Plastic { get; private set; }
        public int Paper { get; private set; }
        public int Can { get; private set; }

        public ISeries[] PieSeries { get; private set; }
        public ISeries[] ColumnSeries { get; private set; }
        public Axis[] XAxes { get; private set; }

        public ViewModel()
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
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = new string[] { "" },
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
                        Plastic = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Models.Result.RESULT_SELECT_PAPER, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        Paper = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Models.Result.RESULT_SELECT_CAN, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        Can = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }

                    // PieSeries 업데이트
                    PieSeries = new ISeries[]
                    {
                        new PieSeries<double>
                        {
                            Name = "PLASTIC",
                            Values = new double[] { Plastic != 0 ? Plastic : double.NaN },
                            Fill = new SolidColorPaint(SKColor.Parse("#D0E1E9"))
                        },
                        new PieSeries<double>
                        {
                            Name = "PAPER",
                            Values = new double[] { Paper != 0 ? Paper : double.NaN },
                            Fill = new SolidColorPaint(SKColor.Parse("#A1C2D5"))
                        },
                        new PieSeries<double>
                        {
                            Name = "CAN",
                            Values = new double[] { Can != 0 ? Can : double.NaN },
                            Fill = new SolidColorPaint(SKColor.Parse("#5F7290"))
                        }
                    };

                    // ColumnSeries 업데이트
                    ColumnSeries = new ISeries[]
                    {
                        new ColumnSeries<double>
                        {
                            Name = "Plastic",
                            Values = new double[] { Plastic },
                            Fill = new SolidColorPaint(SKColor.Parse("#D0E1E9"))

                        },
                        new ColumnSeries<double>
                        {
                            Name = "Paper",
                            Values = new double[] { Paper },
                            Fill = new SolidColorPaint(SKColor.Parse("#A1C2D5"))
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Can",
                            Values = new double[] { Can },
                            Fill = new SolidColorPaint(SKColor.Parse("#5F7290"))
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
