using System;
using System.Collections.Generic;

using System.Text;

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
    /// Wining.xaml 的交互逻辑
    /// </summary>
    public partial class Wining : Window
    {
		private int L;
		private string Name, Time;
        public Wining(int Level,string CurName,string CurTime)
        {
            L = Level;
			Name = CurName;
			Time = CurTime;
            InitializeComponent();
        }

        private void 重新选择难度_Click(object sender, RoutedEventArgs e)
        {
            难度选择 c = new 难度选择();
            c.Show();
            this.Close();
        }

        private void 退出游戏_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

		private void button_Click(object sender, RoutedEventArgs e)
		{
			HeroRank Rank = new HeroRank(L,Name,Time);
			Rank.Show();
		}
	}
}
