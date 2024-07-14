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
using SmartFactory.Helpers;
using SmartFactory.Models;
using System.Windows.Media.Animation;

namespace SmartFactory.Views
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            this.Loaded += Home_Loaded;
        }

        // 창 호출 애니메이션
        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.8);

            this.BeginAnimation(UserControl.OpacityProperty, fadeInAnimation);
        }
    }
}

    

       

