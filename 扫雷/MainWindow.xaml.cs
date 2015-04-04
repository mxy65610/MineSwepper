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
using System.IO;
namespace 扫雷
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
		private string[] NameInit = new string[] { "匿名", "匿名", "匿名" };
		private string[] TimeInit = new string[] { "999", "999", "999" }; 
        public MainWindow()
        {
			
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "rankname.dat")==false)
				File.Create(AppDomain.CurrentDomain.BaseDirectory + "rankname.dat");
			if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ranktime.dat") == false)
				File.Create(AppDomain.CurrentDomain.BaseDirectory + "ranktime.dat");
			FileInfo NameInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "rankname.dat");
			FileInfo TimeInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "ranktime.dat");
			if (NameInfo.Length==0||TimeInfo.Length==0)
			{
				RankFileInit();
			}
            InitializeComponent();
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            难度选择 c = new 难度选择();
            c.Show();
            this.Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			HeroRank Rank = new HeroRank();
			Rank.Show();
		}

		private void RankFileInit()
		{
			StreamWriter NameWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "rankname.dat");
			StreamWriter TimeWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "ranktime.dat");

			foreach(string t in NameInit)
			{
				NameWriter.WriteLine(t);
			}
			foreach(string t in TimeInit)
			{
				TimeWriter.WriteLine(t);
			}
			NameWriter.Close();
			TimeWriter.Close();
		}
	}
}
