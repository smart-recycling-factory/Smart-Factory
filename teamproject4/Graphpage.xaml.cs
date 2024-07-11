using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel; // 추가된 네임스페이스
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
using teamproject4.Helpers;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace teamproject4
{
    public partial class Graphpage : Window
    {
        public ViewModel ViewModel { get; private set; }

        public Graphpage()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            DataContext = ViewModel;

            ViewModel.get_data_graph();
        }

        private void Pixelchart_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        // Home 버튼 클릭
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            homepage homepage = new homepage();
            homepage.Topmost = true;
            homepage.ShowDialog();
        }

        // Graph 버튼 클릭
        private void BtnGraph_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Graphpage graphpage = new Graphpage();
            graphpage.Topmost = true;
            graphpage.ShowDialog();
        }

        // Management 클릭
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
    }

    public class ViewModel
    {
        public int red { get; private set; }
        public int green { get; private set; }
        public int blue { get; private set; }

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
                        red = P_result != null ? Convert.ToInt32(P_result) : 0;
                    }

                    using (var Rcount = new SqlCommand(Result.RESULT_SELECT_PAPER, conn))
                    {
                        var R_result = Rcount.ExecuteScalar();
                        green = R_result != null ? Convert.ToInt32(R_result) : 0;
                    }

                    using (var Ccount = new SqlCommand(Result.RESULT_SELECT_CAN, conn))
                    {
                        var C_result = Ccount.ExecuteScalar();
                        blue = C_result != null ? Convert.ToInt32(C_result) : 0;
                    }

                    // Series 속성 업데이트 및 색상 설정
                    Series = new ISeries[]
                    {
                        new PieSeries<double>
                        {
                            Values = new double[] { red },
                            Fill = new SolidColorPaint(SKColors.Red) // 빨간색
                        },
                        new PieSeries<double>
                        {
                            Values = new double[] { green },
                            Fill = new SolidColorPaint(SKColors.Green) // 초록색
                        },
                        new PieSeries<double>
                        {
                            Values = new double[] { blue },
                            Fill = new SolidColorPaint(SKColors.Blue) // 파란색
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
