using Feelwords.Logic;
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

namespace Feelwords.WPF
{
	/// <summary>
	/// Interaction logic for Game.xaml
	/// </summary>
	public partial class Game : Window
	{
		private GameField gameField { get; set; }
        Random rd = new Random();
        private int n = 5;
		private int m = 5;
        private bool activeMouseLeftButtonDown = false;
        List<string> listWords = new List<string>();
        List<Word> word = new List<Word>();
        GameInfo gameInfo;
        Brush brush;
        TextBox tbox_PlayerName;

        public Game(TextBox tbox_PlayerName)
		{
			InitializeComponent();
            this.tbox_PlayerName = tbox_PlayerName;
            gameInfo = new GameInfo(n, m, tbox_PlayerName.Text, 0);
            //gameField = new GameField(canvas, lbl_Score);
        }

		private void btn_Hint_Click(object sender, RoutedEventArgs e)
		{
            ResettingTheSelection();
            Random rd = new Random();
            int k = rd.Next(0, word.Count);
            for (int i = 0; i < canvas.Children.Count; i++)
			{
				if (word[k].ControlName((canvas.Children[i] as TextBlock).Name))
					(canvas.Children[i] as TextBlock).Background = Brushes.Yellow;
			}
		}

        private void btn_Back_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new();
			mainWindow.Show();
			Close();
		}

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            //canvas.Columns = m;
            //canvas.Rows = n;
            //MainGrid.Children.Add(UniGrid);
            
            int[,] Matrix = new int[n, m];
            MatrixHelper mh = new MatrixHelper(Matrix);
            Matrix = mh.Slise(5);
            Dictionary<int, Color> colorMap = GetColorMap(Matrix);
            var worms = mh.getDictionary();
            List<int> countWord = new List<int>(); // Список количества слов и их размеры
			foreach (var item in worms)
			{
                countWord.Add(item.Value.Count);
            }
            WorkWithFiles workWithFiles = new WorkWithFiles(tbox_PlayerName.Text, Convert.ToInt64(lbl_Score.Content));
            listWords = workWithFiles.ReadWordsFromFile(countWord); //Список слов
            int l = 0;
			foreach (var item in worms)
			{
				Word word1 = new Word();
				word1.Name = listWords[l];
                lbl_Words.Content += listWords[l] + "\t";
                word1.CharName = listWords[l].ToArray();
				foreach (var it in item.Value)
				{
					word1.X.Add(it.x);
					word1.Y.Add(it.y);
				}
                word.Add(word1);
                l++;
			}
            listWords.Clear();
            gameInfo.DictionaryWord = word;
            
			//int words = Slise(Matrix);
			for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {

                    //Debug.Write(Matrix[i,j] + "\t");
                    //Rectangle rec = new Rectangle();
                    //rec.Stroke = Brushes.Brown;
                    //rec.StrokeThickness=1;

                    //rec.Fill=new SolidColorBrush(colorMap[Matrix[i,j]]);

                    //UniGrid.Children.Add(rec);
                    int index = Matrix[i, j];
                    int k = 0;
                    for (k = 0; k < worms[index].Count; k++)
                    {
                        if (worms[index][k] == new MatrixHelper.IntPoint(i, j)) break;
                    }
                    TextBlock textBlock = new TextBlock();
                    //textBlock.Background = new SolidColorBrush(colorMap[Matrix[i, j]]);
					foreach (var item in word)
					{
                        string letter = item.Letter(i, j);
                        if (letter == "Координаты не верные")
						{
                            continue;
						}
                        textBlock.Text = letter;
                        break;
					}
                    textBlock.TextAlignment = TextAlignment.Center;
					//textBlock.HorizontalAlignment = HorizontalAlignment.Center;
					//textBlock.VerticalAlignment = VerticalAlignment.Center;
					textBlock.Width = canvas.Width / n - 10;
                    textBlock.Height = canvas.Height / m - 10;
                    if (!double.IsNaN(textBlock.Width) || !double.IsNaN(textBlock.Height))
                        SetFontSize(textBlock);
                    textBlock.Name = "tb" + i + j;
					Canvas.SetLeft(textBlock, i * canvas.Width / n);
					Canvas.SetTop(textBlock, j * canvas.Height / m);
					canvas.Children.Add(textBlock);
                }
                //Debug.WriteLine("");
            }

        }
   //     private Word GetWord(string listWords, List<MatrixHelper.IntPoint> worms)
   //     {
   //         Word word = new Word();
			//word.Name = listWords;
			//foreach (var item in worms)
			//{
			//	word.XY.Add(item.ToString());
			//}
			//return word;
   //     }
        private void SetFontSize(TextBlock textBlock)
        {
            if (textBlock.Width < textBlock.Height)
                textBlock.FontSize = textBlock.Width * 0.60;
            else
                textBlock.FontSize = textBlock.Height * 0.60;
        }

        private Dictionary<int, Color> GetColorMap(int[,] matrix)
        {
            var colorMap = new Dictionary<int, Color>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (colorMap.ContainsKey(matrix[i, j])) continue;
                    Color randomColor = new Color();
                    randomColor.R = (byte)rd.Next(0, 255);
                    randomColor.G = (byte)rd.Next(0, 255);
                    randomColor.B = (byte)rd.Next(0, 255);
                    randomColor.A = 255;
                    colorMap.Add(matrix[i, j], randomColor);
                }
            }

            return colorMap;
        }

        private int Slise(int[,] Matrix)
        {
            int k = 0;
            int x = 0;
            int y = 0;
            int red = 1;
            Matrix[0, 0] = red;
            while (FindNextPoint(ref x, ref y, Matrix))
            {
                red += 2;
                Matrix[x, y] = red;

            }

            return k;
        }

        private bool FindNextPoint(ref int x, ref int y, int[,] matrix)
        {
            List<Directions> posible = new List<Directions>();
            if (x - 1 > -1 && matrix[x - 1, y] == 0)
            {
                posible.Add(Directions.Left);

            }

            if (x + 1 < matrix.GetLength(1) && matrix[x + 1, y] == 0)
            {
                posible.Add(Directions.Right);

            }

            if (y - 1 > -1 && matrix[x, y - 1] == 0)
            {
                posible.Add(Directions.Top);
            }

            if (y + 1 < matrix.GetLength(0) && matrix[x, y + 1] == 0)
            {
                posible.Add(Directions.Bottom);
            }

            if (posible.Count == 0) return false;

            int posCount = rd.Next(0, posible.Count);
            switch (posible[posCount])
            {
                case Directions.Bottom:
                    y = y + 1;
                    break;
                case Directions.Top:
                    y = y - 1;
                    break;
                case Directions.Right:
                    x = x + 1;
                    break;
                case Directions.Left:
                    x = x - 1;
                    break;
            }
            return true;
        }

		private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
            if (activeMouseLeftButtonDown)
            {
                activeMouseLeftButtonDown = false;
                bool check = WordVerification(listWords);
                if (check)
                {
                    for (int i = 0; i < listWords.Count; i++)
                    {
                        for (int j = 0; j < canvas.Children.Count; j++)
                        {
                            if ((canvas.Children[j] as TextBlock).Name == listWords[i])
                            {
                                (canvas.Children[j] as TextBlock).Background = Brushes.Green;
                                break;
                            }
                        }
                    }
                    gameInfo.Score += 100;
                    lbl_Score.Content = gameInfo.Score;
                }
				else
				{
					ResettingTheSelection();
				}
				listWords.Clear();
            }
            else
			{
                activeMouseLeftButtonDown = true;
				brush = ((TextBlock)(e.OriginalSource as FrameworkElement)).Background;
                ((TextBlock)(e.OriginalSource as FrameworkElement)).Background = Brushes.Cyan;
                listWords.Add((e.OriginalSource as FrameworkElement).Name);
            }
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
            if (activeMouseLeftButtonDown)
            {
                ((TextBlock)(e.OriginalSource as FrameworkElement)).Background = Brushes.Cyan;
				if (!listWords.Contains((e.OriginalSource as FrameworkElement).Name))
				{
                    listWords.Add((e.OriginalSource as FrameworkElement).Name);
				}
            }
        }

		private void canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
            ResettingTheSelection();
        }
        private void ResettingTheSelection()
		{
			activeMouseLeftButtonDown = false;
			for (int i = 0; i < canvas.Children.Count; i++)
			{
				foreach (var c in listWords)
				{
					if ((canvas.Children[i] as TextBlock).Name.Contains(c))
						(canvas.Children[i] as TextBlock).Background = brush;
				}
			}
			listWords.Clear();
		}
        private bool WordVerification(List<string> word1)
        {
			foreach (var item in word)
			{
				if (item.CheckingTheWord(word1))
				{
                    return true;
				}
			}
            return false;
		}
    }
}
