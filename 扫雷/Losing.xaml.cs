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
    /// Losing.xaml 的交互逻辑
    /// </summary>
    public partial class Losing : Window
    {
        public Losing()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            难度选择 c = new 难度选择();
            c.Show();
            this.Close();
        }

        private void 退出游戏_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
