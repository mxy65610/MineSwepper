using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace 扫雷
{
    /// <summary>
    /// PaneField.xaml 的交互逻辑
    /// </summary>
    public partial class PaneField : Window
    {
		
        private int seconds = 0;
        private StringBuilder myTimerStr=new StringBuilder("");
        private DispatcherTimer myTimer = new DispatcherTimer();
        private bool WiningJudge = true;
        private bool LosingJudge = true;
        private bool IsGameOver = false;
        private bool IsTimerStart = false;
        private int L;
        private ImageSource image = new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Marked.png"));
        private TextBlock textBlock = new TextBlock();
		private string CurName;
        public PaneField()
        {
            
            InitializeComponent();
            
        }
        Pane[,] paneField;
        public PaneField(int level,string Name)
        {
            L = level;
			CurName = Name;
            InitializeComponent();
            if (level == 0) paneField = new Pane[9, 9];
            else if (level == 1) paneField = new Pane[16, 16];
            else if (level == 2) paneField = new Pane[16, 30];
            textBox.Text = myTimerStr.ToString();
            textBox.IsEnabled = false;
            myTimer.Interval = TimeSpan.FromSeconds(1);
            myTimer.Tick += MyTimer_Tick;
            textBox.Text = "0";
            MineInit(level);
            MineAroundInit(level);
            MineButtonInit(level);
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            seconds++;
            myTimerStr = new StringBuilder(seconds.ToString());
            textBox.Text = myTimerStr.ToString();
        }
        private void MineInit(int level)
        {
            int r, count = 0;
            Random rand = new Random();
            #region 初级
            if (level == 0)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        paneField[i, j] = new Pane(false);
                        paneField[i, j].IsFlagged = paneField[i, j].IsOpened = false;
                        paneField[i, j].MaxRow = paneField[i, j].MaxColumn = 9;
                        paneField[i, j].MaxNoMineNum = 71;
                    }
                while (true)
                {
                    r = rand.Next(81);
                    if (paneField[r / 9, r % 9].IsMine == false)
                    {
                        paneField[r / 9, r % 9].IsMine = true;
                        count++;
                    }
                    if (count == 10) break;
                }
            }
            #endregion
            #region 中级
            else if (level == 1)
            {
                for (int i = 0; i < 16; i++)
                    for (int j = 0; j < 16; j++)
                    {
                        paneField[i, j] = new Pane(false);
                        paneField[i, j].IsFlagged = paneField[i, j].IsOpened = false;
                        paneField[i, j].MaxRow = paneField[i, j].MaxColumn = 16;
                        paneField[i, j].MaxNoMineNum = 216;
                    }
                while (true)
                {
                    r = rand.Next(256);
                    if (paneField[r / 16, r % 16].IsMine == false)
                    {
                        paneField[r / 16, r % 16].IsMine = true;
                        count++;
                    }
                    if (count == 40) break;
                }
            }
            #endregion
            #region 高级
            else if (level == 2)
            {
                for (int i = 0; i < 16; i++)
                    for (int j = 0; j < 30; j++)
                    {
                        paneField[i, j] = new Pane(false);
                        paneField[i, j].IsFlagged = paneField[i, j].IsOpened = false;
                        paneField[i, j].MaxRow = 16;
                        paneField[i, j].MaxColumn = 30;
                        paneField[i, j].MaxNoMineNum = 381;
                    }
                while (true)
                {
                    r = rand.Next(480);
                    if (paneField[r / 30, r % 30].IsMine == false)
                    {
                        paneField[r / 30, r % 30].IsMine = true;
                        count++;
                    }
                    if (count == 99) break;
                }
            }
            #endregion
        }
        private void MineAroundInit(int level)
        {

            #region 初级
            if (level == 0)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        int counter = 0;

                        #region 四个角的处理
                        if (i == 0 && j == 0) //LeftUp Corner
                        {
                            if (paneField[0, 1].IsMine == true) counter++;
                            if (paneField[1, 0].IsMine == true) counter++;
                            if (paneField[1, 1].IsMine == true) counter++;
                        }
                        else if (i == 0 && j == 8) //RightUp Corner
                        {
                            if (paneField[0, 7].IsMine == true) counter++;
                            if (paneField[1, 7].IsMine == true) counter++;
                            if (paneField[1, 8].IsMine == true) counter++;
                        }
                        else if (i == 8 && j == 0) //LeftDown Corner
                        {
                            if (paneField[7, 0].IsMine == true) counter++;
                            if (paneField[7, 1].IsMine == true) counter++;
                            if (paneField[8, 1].IsMine == true) counter++;
                        }
                        else if (i == 8 && j == 8) //RightDown Corner
                        {
                            if (paneField[7, 7].IsMine == true) counter++;
                            if (paneField[7, 8].IsMine == true) counter++;
                            if (paneField[8, 7].IsMine == true) counter++;
                        }
                        #endregion
                        #region 四条边的处理
                        else if (i == 0 && j != 0 && j != 8) //UpEdge
                        {
                            for (int p = i; p <= i + 1; p++)
                                for (int q = j - 1; q <= j + 1; q++)
                                {
                                    if (!(p==i&&q==j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        else if (i == 8 && j != 0 && j != 8) //DownEdge
                        {
                            for (int p = i - 1; p <= i; p++)
                                for (int q = j - 1; q <= j + 1; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        else if (j == 0 && i != 0 && i != 8) //LeftEdge
                        {
                            for (int p = i - 1; p <= i + 1; p++)
                                for (int q = j; q <= j + 1; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        else if (j == 8 && i != 0 && i != 8) //RightEdge
                        {
                            for (int p = i - 1; p <= i + 1; p++)
                                for (int q = j - 1; q <= j; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        #endregion
                        #region 其他位置的处理
                        else
                            for (int p = i - 1; p <= i + 1; p++)
                                for (int q = j - 1; q <= j + 1; q++)
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                        #endregion

                        paneField[i, j].MineAround = counter;
                    }
            }
            #endregion
            #region 中级
            else if (level == 1)
            {
                for (int i = 0; i < 16; i++)
                    for (int j = 0; j < 16; j++)
                    {
                        int counter = 0;

                        #region 四个角的处理
                        if (i == 0 && j == 0) //LeftUp Corner
                        {
                            if (paneField[0, 1].IsMine == true) counter++;
                            if (paneField[1, 0].IsMine == true) counter++;
                            if (paneField[1, 1].IsMine == true) counter++;
                        }
                        else if (i == 0 && j == 15) //RightUp Corner
                        {
                            if (paneField[0, 14].IsMine == true) counter++;
                            if (paneField[1, 14].IsMine == true) counter++;
                            if (paneField[1, 14].IsMine == true) counter++;
                        }
                        else if (i == 15 && j == 0) //LeftDown Corner
                        {
                            if (paneField[14, 0].IsMine == true) counter++;
                            if (paneField[14, 1].IsMine == true) counter++;
                            if (paneField[15, 1].IsMine == true) counter++;
                        }
                        else if (i == 15 && j == 15) //RightDown Corner
                        {
                            if (paneField[14, 14].IsMine == true) counter++;
                            if (paneField[14, 15].IsMine == true) counter++;
                            if (paneField[15, 14].IsMine == true) counter++;
                        }
                        #endregion
                        #region 四条边的处理
                        else if (i == 0 && j != 0 && j != 15) //UpEdge
                        {
                            for (int p = i; p <= i + 1; p++)
                                for (int q = j - 1; q <= j + 1; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        else if (i == 15 && j != 0 && j != 15) //DownEdge
                        {
                            for (int p = i - 1; p <= i; p++)
                                for (int q = j - 1; q <= j + 1; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        else if (j == 0 && i != 0 && i != 15) //LeftEdge
                        {
                            for (int p = i - 1; p <= i + 1; p++)
                                for (int q = j; q <= j + 1; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        else if (j == 15 && i != 0 && i != 15) //RightEdge
                        {
                            for (int p = i - 1; p <= i + 1; p++)
                                for (int q = j - 1; q <= j; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        #endregion
                        #region 其他位置的处理
                        else
                            for (int p = i - 1; p <= i + 1; p++)
                                for (int q = j - 1; q <= j + 1; q++)
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                        #endregion

                        paneField[i, j].MineAround = counter;
                    }
            }
            #endregion
            #region 高级
            else if (level == 2)
            {
                for (int i = 0; i < 16; i++)
                    for (int j = 0; j < 30; j++)
                    {
                        int counter = 0;

                        #region 四个角的处理
                        if (i == 0 && j == 0) //LeftUp Corner
                        {
                            if (paneField[0, 1].IsMine == true) counter++;
                            if (paneField[1, 0].IsMine == true) counter++;
                            if (paneField[1, 1].IsMine == true) counter++;
                        }
                        else if (i == 0 && j == 29) //RightUp Corner
                        {
                            if (paneField[0, 28].IsMine == true) counter++;
                            if (paneField[1, 28].IsMine == true) counter++;
                            if (paneField[1, 29].IsMine == true) counter++;
                        }
                        else if (i == 15 && j == 0) //LeftDown Corner
                        {
                            if (paneField[14, 0].IsMine == true) counter++;
                            if (paneField[14, 1].IsMine == true) counter++;
                            if (paneField[15, 1].IsMine == true) counter++;
                        }
                        else if (i == 15 && j == 29) //RightDown Corner
                        {
                            if (paneField[14, 28].IsMine == true) counter++;
                            if (paneField[14, 29].IsMine == true) counter++;
                            if (paneField[15, 28].IsMine == true) counter++;
                        }
                        #endregion
                        #region 四条边的处理
                        else if (i == 0 && j != 0 && j != 29) //UpEdge
                        {
                            for (int p = i; p <= i + 1; p++)
                                for (int q = j - 1; q <= j + 1; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        else if (i == 15 && j != 0 && j != 29) //DownEdge
                        {
                            for (int p = i - 1; p <= i; p++)
                                for (int q = j - 1; q <= j + 1; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        else if (j == 0 && i != 0 && i != 15) //LeftEdge
                        {
                            for (int p = i - 1; p <= i + 1; p++)
                                for (int q = j; q <= j + 1; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        else if (j == 29 && i != 0 && i != 15) //RightEdge
                        {
                            for (int p = i - 1; p <= i + 1; p++)
                                for (int q = j - 1; q <= j; q++)
                                {
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                                }
                        }
                        #endregion
                        #region 其他位置的处理
                        else
                            for (int p = i - 1; p <= i + 1; p++)
                                for (int q = j - 1; q <= j + 1; q++)
                                    if (!(p == i && q == j))
                                        if (paneField[p, q].IsMine == true) counter++;
                        #endregion

                        paneField[i, j].MineAround = counter;
                    }
            }
            #endregion

        }
        private void MineButtonInit(int level)
        {
            #region 初级
            if (level == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (paneField[i, j].IsMine == true)
                            paneField[i, j].back.Source =
                                new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Mine.PNG"));
                        else
                            switch (paneField[i, j].MineAround)
                            {

                                case 0:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num0.bmp"));
                                        break;
                                    }
                                case 1:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num1.PNG"));
                                        break;
                                    }
                                case 2:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num2.PNG"));
                                        break;
                                    }
                                case 3:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num3.PNG"));
                                        break;
                                    }
                                case 4:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num4.PNG"));
                                        break;
                                    }
                                case 5:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num5.PNG"));
                                        break;
                                    }
                                case 6:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num6.PNG"));
                                        break;
                                    }
                                case 7:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num7.PNG"));
                                        break;
                                    }
                                case 8:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num8.PNG"));
                                        break;
                                    }
                            }
                    }
                }
            }
            #endregion
            #region 中级
            if (level == 1)
            {
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        if (paneField[i, j].IsMine == true)
                            paneField[i, j].back.Source =
                                new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Mine.PNG"));
                        else
                            switch (paneField[i, j].MineAround)
                            {

                                case 0:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num0.bmp"));
                                        break;
                                    }
                                case 1:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num1.PNG"));
                                        break;
                                    }
                                case 2:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num2.PNG"));
                                        break;
                                    }
                                case 3:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num3.PNG"));
                                        break;
                                    }
                                case 4:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num4.PNG"));
                                        break;
                                    }
                                case 5:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num5.PNG"));
                                        break;
                                    }
                                case 6:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num6.PNG"));
                                        break;
                                    }
                                case 7:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num7.PNG"));
                                        break;
                                    }
                                case 8:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num8.PNG"));
                                        break;
                                    }
                            }
                    }
                }
            }
            #endregion
            #region 高级
            if (level == 2)
            {
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        if (paneField[i, j].IsMine == true)
                            paneField[i, j].back.Source =
                                new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Mine.PNG"));
                        else
                            switch (paneField[i, j].MineAround)
                            {

                                case 0:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num0.bmp"));
                                        break;
                                    }
                                case 1:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num1.PNG"));
                                        break;
                                    }
                                case 2:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num2.PNG"));
                                        break;
                                    }
                                case 3:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num3.PNG"));
                                        break;
                                    }
                                case 4:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num4.PNG"));
                                        break;
                                    }
                                case 5:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num5.PNG"));
                                        break;
                                    }
                                case 6:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num6.PNG"));
                                        break;
                                    }
                                case 7:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num7.PNG"));
                                        break;
                                    }
                                case 8:
                                    {
                                        paneField[i, j].back.Source =
                                            new BitmapImage(new Uri(@"F:\扫雷\扫雷\扫雷\Properties\Resources\Num8.PNG"));
                                        break;
                                    }
                            }
                    }
                }
            }
            #endregion

            #region 初级
            if(level==0)
            {
                AddPane(9, 9);
            }
            #endregion
            #region 中级
            else if(level==1)
            {
                AddPane(16, 16);
            }
            #endregion
            #region 高级
            else if(level==2)
            {
                AddPane(16, 30);
            }
            #endregion
           
        }

        private void AddPane(int Row,int Column)
        {
            for(int i=0;i<Row;i++)
            {
                for(int j=0;j<Column;j++)
                {
                    Canvas.SetLeft(paneField[i, j], j * 576 / Column);
                    Canvas.SetTop(paneField[i, j], i * 576 / Row);
                    Canvas.SetLeft(paneField[i, j].back, j * 576 / Column);
                    Canvas.SetTop(paneField[i, j].back, i * 576 / Row);
                    paneField[i, j].Width = 576 / Column;
                    paneField[i, j].Height = 576 / Row;
                    paneField[i, j].back.Width = 576 / Column;
                    paneField[i, j].back.Height = 576 / Row;
                    paneField[i, j].back.Stretch = Stretch.Fill;
                    paneField[i, j].Click += PaneField_Click;
                    paneField[i, j].MouseRightButtonDown += PaneField_MouseDown;
                    Field.Children.Add(paneField[i, j]);
                }
            }
        }
        private void PaneField_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.RightButton == MouseButtonState.Pressed)
            {
                if ((sender as Pane).IsFlagged == false)
                {
                    (sender as Pane).IsFlagged = true;
                    (sender as Pane).Background = new ImageBrush(image);
                    (sender as Pane).Foreground = new ImageBrush(image);
                }
                else if ((sender as Pane).IsFlagged == true)
                {
                    (sender as Pane).IsFlagged = false;
                    (sender as Pane).Background = null;
                    (sender as Pane).Foreground = null;
                }
            }

        }

        private void PaneField_Click(object sender, RoutedEventArgs e)
        {
            if(IsTimerStart==false)
            {
                IsTimerStart = true;
                myTimer.Start();
            }
            if(IsWining()&&WiningJudge==true&&IsGameOver==false)
            {
                IsGameOver = true;
                WiningJudge = false;
                for (int i = 0; i < paneField[0,0].MaxRow; i++)
                    for (int j = 0; j < paneField[0,0].MaxColumn; j++)
                    {
                        PaneField_Click(paneField[i, j], e);
                    }
                System.Threading.Thread.Sleep(2000);
                Wining w = new Wining(L,CurName,seconds.ToString());
                w.Show();
                System.Threading.Thread.Sleep(3000);
                this.Close();
            }
             if (!Field.Children.Contains((sender as Pane).back))
            {
                int Row, Column;
                Pane t = (sender as Pane);
                (sender as Pane).IsOpened = true;
                Field.Children.Remove((UIElement)sender);
                Field.Children.Add((sender as Pane).back);
                if(t.IsMine==true && LosingJudge==true&&IsGameOver==false)
                {
                    IsGameOver = true;
                    LosingJudge = false;
                    for(int i=0;i<t.MaxRow;i++)
                        for(int j=0;j<t.MaxColumn;j++)
                        {
                            
                            PaneField_Click(paneField[i, j], e);
                        }
                    Losing l = new Losing();
                    l.Show();
                    Thread.Sleep(3000);
                    this.Close();
                }
                //递归
                if (t.MineAround == 0)
                {
                    GetRowAndColumn(t, out Row, out Column, t.MaxRow, t.MaxColumn);
                    #region 四个角
                    if (Row == 0 && Column == 0 && t.IsMine == false) //左上角
                    {
                        PaneField_Click(paneField[0, 1], e);
                        PaneField_Click(paneField[1, 0], e);
                        PaneField_Click(paneField[1, 1], e);
                    }
                    else if (Row == 0 && Column == t.MaxColumn - 1 && t.IsMine == false) //右上角
                    {
                        PaneField_Click(paneField[0, t.MaxColumn - 2], e);
                        PaneField_Click(paneField[1, t.MaxColumn - 2], e);
                        PaneField_Click(paneField[1, t.MaxColumn - 1], e);
                    }
                    else if (Row == t.MaxRow - 1 && Column == 0 && t.IsMine == false) //左下角
                    {
                        PaneField_Click(paneField[t.MaxRow - 2, 0], e);
                        PaneField_Click(paneField[t.MaxRow - 2, 1], e);
                        PaneField_Click(paneField[t.MaxRow - 1, 1], e);
                    }
                    else if (Row == t.MaxRow - 1 && Column == t.MaxColumn - 1 && t.IsMine == false) //右下角
                    {
                        PaneField_Click(paneField[t.MaxRow - 2, t.MaxColumn - 2], e);
                        PaneField_Click(paneField[t.MaxRow - 2, t.MaxColumn - 1], e);
                        PaneField_Click(paneField[t.MaxRow - 1, t.MaxColumn - 2], e);
                    }
                    #endregion
                    #region 四条边
                    else if (Row == 0 && Column != 0 && Column != t.MaxColumn - 1 && t.IsMine == false) //上边
                    {
                        for (int p = Row; p <= Row + 1; p++)
                            for (int q = Column - 1; q <= Column + 1; q++)
                            {
                                bool b = Field.Children.Contains(paneField[p, q].back);
                                if (!(p == Row && q == Column) && b == false)
                                    PaneField_Click(paneField[p, q], e);
                            }
                    }
                    else if (Row == t.MaxRow - 1 && Column != 0 && Column != t.MaxColumn - 1 && t.IsMine == false) //下边
                    {
                        for (int p = Row - 1; p <= Row; p++)
                            for (int q = Column - 1; q <= Column + 1; q++)
                            {
                                bool b = Field.Children.Contains(paneField[p, q].back);
                                if (!(p == Row && q == Column) && b == false)
                                    PaneField_Click(paneField[p, q], e);
                            }
                    }
                    else if (Column == 0 && Row != t.MaxRow - 1 && Row != 0 && t.IsMine == false) //左边
                    {
                        for (int p = Row - 1; p <= Row + 1; p++)
                            for (int q = Column; q <= Column + 1; q++)
                            {
                                bool b = Field.Children.Contains(paneField[p, q].back);
                                if (!(p == Row && q == Column) && b == false)
                                    PaneField_Click(paneField[p, q], e);
                            }
                    }
                    else if (Column == t.MaxColumn - 1 && Row != t.MaxRow - 1 && Row != 0 && t.IsMine == false) //右边
                    {
                        for (int p = Row - 1; p <= Row + 1; p++)
                            for (int q = Column - 1; q <= Column; q++)
                            {
                                bool b = Field.Children.Contains(paneField[p, q].back);
                                if (!(p == Row && q == Column) && b == false)
                                    PaneField_Click(paneField[p, q], e);
                            }
                    }
                    #endregion
                    #region 其他位置
                    else if (t.IsMine == false)
                        for (int p = Row - 1; p <= Row + 1; p++)
                            for (int q = Column - 1; q <= Column + 1; q++)
                            {
                                bool b = Field.Children.Contains(paneField[p, q].back);
                                if (!(p == Row && q == Column) && b == false)
                                    PaneField_Click(paneField[p, q], e);
                            }
                    #endregion
                }
            }
        }
        private void GetRowAndColumn(Pane p, out int Row, out int Column, int maxRow,int maxColumn)
        {
            int i=0, j=0;
            bool flag = false;

            for(i=0;i<maxRow;i++)
            {
                for(j=0;j<maxColumn;j++)
                {
                    if(paneField[i,j].Equals(p))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == true) break;
            }
            Row = i;
            Column = j;
        }
        private void ReInit_Click(object sender, RoutedEventArgs e)
        {
            seconds = 0;
            myTimer.Stop();
            IsTimerStart = false;
            myTimerStr = new StringBuilder(seconds.ToString());
            textBox.Text = myTimerStr.ToString();
            MineInit(L);
            MineAroundInit(L);
            MineButtonInit(L);
        }

        private bool IsWining()
        {
            int counter = 0;
            for(int i=0;i<paneField[0,0].MaxRow;i++)
                for(int j=0;j<paneField[0,0].MaxColumn;j++)
                {
                    if (paneField[i, j].IsOpened == true)
                        counter++;
                }
            if (counter == paneField[0, 0].MaxNoMineNum - 1) return true;
            else return false;
        }

        
    }
}
