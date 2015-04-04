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
using System.Windows.Shapes;
using System.Collections;
using System.IO;
namespace 扫雷
{
	/// <summary>
	/// Interaction logifor HeroRank.xaml
	/// </summary>
	public partial class HeroRank : Window
	{
		private string[] Name = new string[3];
		private string[] Time = new string[3];
		
		#region 姓名定义
		public string EasyFirstName = "匿名";
		public string MidiumFirstName = "匿名";
		public string HardFirstName = "匿名";
		#endregion

		#region 时间定义
		public int EasyFirstTime = 999;
		public int MidiumFirstTime = 999;
		public int HardFirstTime = 999;
		#endregion
		public HeroRank(int Level,string CurName,string CurTime)
		{

			Read();
			Name[Level] = CurName;
			Time[Level] = CurTime;
			InitializeComponent();
			
			
			EasyFirstNameL.Content = Name[0];
			EasyFirstTimeL.Content = Time[0];
			MidiumFirstNameL.Content = Name[1];
			MidiumFirstTimeL.Content = Time[1];
			HardFirstNameL.Content = Name[2];
            HardFirstTimeL.Content = Time[2];
			Write();
		}

		public HeroRank()
		{

			Read();
			InitializeComponent();

			EasyFirstNameL.Content = Name[0];
			EasyFirstTimeL.Content = Time[0];
			MidiumFirstNameL.Content = Name[1];
			MidiumFirstTimeL.Content = Time[1];
			HardFirstNameL.Content = Name[2];
			HardFirstTimeL.Content = Time[2];
		}
		private void Read()
		{
			StreamReader NameReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "rankname.dat");
			StreamReader TimeReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "ranktime.dat");
			for(int i = 0 ; i < 3 ; i++)
			{
				Name[i] = NameReader.ReadLine();
				Time[i] = TimeReader.ReadLine();
			}
			NameReader.Close();
			TimeReader.Close();
		}

		private void Write()
		{
			StreamWriter NameWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "rankname.dat");
			StreamWriter TimeWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "ranktime.dat");
			foreach(string t in Name)
			{
				NameWriter.WriteLine(t);
			}
			foreach(string t in Time)
			{
				TimeWriter.WriteLine(t);
			}
			NameWriter.Close();
			TimeWriter.Close();
		}

	}
}
