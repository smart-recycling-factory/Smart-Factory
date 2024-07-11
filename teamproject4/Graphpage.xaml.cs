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
            DataContext = new ViewModel();
        }

        

        private void Pixelchart_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }


        // Home 버튼 클릭
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            firstpage Firstpage = new firstpage();
            Firstpage.Topmost = true;
            Firstpage.ShowDialog();
        }

        // Graph 버튼 클릭
        private void BtnGraph_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Graphpage graphpage = new Graphpage();
            graphpage.Topmost = true;
            graphpage.ShowDialog();
        }


        // 프로그램 종료버튼
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("종료하시겠습니까?", "종료창", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) Environment.Exit(0);
        }

        // 로그인 버튼
        private void TxtLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            membership Membership = new membership();
            Membership.Topmost = true;
            Membership.ShowDialog();
        }
    }

    public class ViewModel
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 2 } },
                new PieSeries<double> { Values = new double[] { 4 } },
                new PieSeries<double> { Values = new double[] { 1 } },
                new PieSeries<double> { Values = new double[] { 4 } },
                new PieSeries<double> { Values = new double[] { 3 } }
            };
    }
}
