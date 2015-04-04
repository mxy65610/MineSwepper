using System;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 扫雷
{
    /// <summary>
    /// 难度选择.xaml 的交互逻辑
    /// </summary>
    public partial class 难度选择 : Window
    {
        public 难度选择()
        {
            InitializeComponent();
        }
        
      
        private void Click_Easy(object sender, RoutedEventArgs e)
        {
            PaneField PF = new PaneField(0,textBox.Text);
            PF.Show();
            this.Close();
        }

        private void Click_Middle(object sender, RoutedEventArgs e)
        {
            PaneField PF = new PaneField(1,textBox.Text);
            PF.Show();
            this.Close();
        }

        private void Click_Hard(object sender, RoutedEventArgs e)
        {
            PaneField PF = new PaneField(2,textBox.Text);
            PF.Show();
            this.Close();
        }
    }
}
