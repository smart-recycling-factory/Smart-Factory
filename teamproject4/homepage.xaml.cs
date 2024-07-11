﻿using System;
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
using System.Windows.Shapes;

namespace teamproject4
{
    /// <summary>
    /// homepage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class homepage : Window
    {
        public homepage()
        {
            InitializeComponent();
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

        // Management 버튼 클릭
        private void BtnManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Topmost = true;
            mainwindow.ShowDialog();
        }

        /* 활성화 안함
        // Login 클릭
        private void TxtLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            membership Membership = new membership();
            Membership.Topmost = true;
            Membership.ShowDialog();
        }
        */

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

        private void BtnHome_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGraph_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtnManagement_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
