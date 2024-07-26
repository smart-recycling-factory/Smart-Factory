using LiveChartsCore.SkiaSharpView.Painting;
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
using SmartFactory.ViewModels;

namespace SmartFactory.Views
{
    /// <summary>
    /// Graph.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Graph : UserControl
    {
        public GraphVM ViewModel { get; private set; }

        public Graph()
        {
            InitializeComponent();
            this.Loaded += Graph_Loaded;
            ViewModel = new GraphVM();
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
}
