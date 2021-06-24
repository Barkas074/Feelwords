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
		private GameInfo gameInfo;
		private Brush brush;
		private List<RatingInfo> ratingInfo;
		private List<string> listWords = new();
		private List<Word> word = new();
		private Word foundWord = new();
		private List<Word> notFoundWord = new();
		private bool activeMouseLeftButtonDown;
		private bool resume;
		private bool checkSave;


		public Game(TextBox tbox_PlayerName, List<RatingInfo> ratingInfo, bool enableCustomField, int fieldWidth = 5, int fieldHeight = 5)
		{
			InitializeComponent();
			gameInfo = new GameInfo(fieldWidth, fieldHeight, tbox_PlayerName.Text, 0, enableCustomField ? 0 : 1);
			this.ratingInfo = ratingInfo;
		}
		public Game(GameInfo gameInfo, List<RatingInfo> ratingInfo)
		{
			InitializeComponent();
			this.gameInfo = gameInfo;
			this.ratingInfo = ratingInfo;
			word = gameInfo.ListOfWords;
			lbl_Score.Content = gameInfo.Score;
			resume = true;
		}
		private void Window_OnLoaded(object sender, RoutedEventArgs e)
		{
			NewGame();
		}
		private void NewGame()
		{
			lbl_Words.Content = string.Empty;
			if (gameInfo.ListOfWords == null || gameInfo.ListOfWords.Count == 0)
				CreateDictionary();
			else
				foreach (Word word in gameInfo.ListOfWords)
					lbl_Words.Content += word.Name + "\t";

			FillGameField();
			if (gameInfo.ListOfFoundWords.Count != 0)
				PaintOverField();
		}
		private void btn_Hint_Click(object sender, RoutedEventArgs e)
		{
			notFoundWord = gameInfo.ListOfWords;
			foreach (Word foundWord in gameInfo.ListOfFoundWords)
			{
				notFoundWord.Remove(foundWord);
			}

			ResettingTheSelection();
			int randomWords = new Random().Next(0, notFoundWord.Count);
			for (int i = 0; i < notFoundWord[randomWords].CharName.Length; i++)
			{
				(canvas.FindName("tb" + notFoundWord[randomWords].X[i] + notFoundWord[randomWords].Y[i]) as TextBlock).Background = (Brush)typeof(Brushes).GetProperty(SettingsInfo.ComboBox_hint).GetValue(null);
				notFoundWord[randomWords].HintUsed = true;
			}
		}
		private void btn_Back_Click(object sender, RoutedEventArgs e)
		{
			SaveResult();
			MainWindow mainWindow = new();
			mainWindow.Show();
			Close();
		}


		private void CreateDictionary()
		{
			int[,] Matrix = new int[gameInfo.FieldWidth, gameInfo.FieldHeight];
			MatrixHelper mh = new(Matrix);
			_ = mh.Slise(5);
			Dictionary<int, List<MatrixHelper.IntPoint>> worms = mh.getDictionary();
			List<int> countWord = new(); // Список количества слов и их размеры
			foreach (KeyValuePair<int, List<MatrixHelper.IntPoint>> item in worms)
			{
				countWord.Add(item.Value.Count);
			}
			WorkWithFiles workWithFiles = new();
			listWords = workWithFiles.ReadWordsFromFile(countWord); //Список слов
			int l = 0;
			foreach (KeyValuePair<int, List<MatrixHelper.IntPoint>> item in worms)
			{
				Word word1 = new();
				word1.Name = listWords[l];
				lbl_Words.Content += listWords[l] + "\t";
				word1.CharName = listWords[l].ToArray();
				foreach (MatrixHelper.IntPoint it in item.Value)
				{
					word1.X.Add(it.x);
					word1.Y.Add(it.y);
				}
				word.Add(word1);
				l++;
			}
			listWords.Clear();
			gameInfo.ListOfWords = word;
		}
		private void FillGameField()
		{
			for (int i = 0; i < gameInfo.FieldWidth; i++)
			{
				for (int j = 0; j < gameInfo.FieldHeight; j++)
				{
					TextBlock textBlock = new TextBlock();
					foreach (Word item in word)
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
					textBlock.Width = canvas.Width / gameInfo.FieldWidth - 10;
					textBlock.Height = canvas.Height / gameInfo.FieldHeight - 10;
					if (!double.IsNaN(textBlock.Width) || !double.IsNaN(textBlock.Height))
						SetFontSize(textBlock);
					textBlock.Name = "tb" + i + j;
					Canvas.SetLeft(textBlock, i * canvas.Width / gameInfo.FieldWidth);
					Canvas.SetTop(textBlock, j * canvas.Height / gameInfo.FieldHeight);
					canvas.Children.Add(textBlock);
					if (canvas.FindName(textBlock.Name) != null)
						canvas.UnregisterName(textBlock.Name);
					canvas.RegisterName(textBlock.Name, textBlock);
				}
			}
		}

		private void PaintOverField()
		{
			foreach (Word word in gameInfo.ListOfFoundWords)
			{
				for (int i = 0; i < word.CharName.Length; i++)
				{
					(canvas.FindName("tb" + word.X[i] + word.Y[i]) as TextBlock).Background = (Brush)typeof(Brushes).GetProperty(SettingsInfo.ComboBox_foundWords).GetValue(null);
				}
			}
		}
		private void SetFontSize(TextBlock textBlock)
		{
			if (textBlock.Width < textBlock.Height)
				textBlock.FontSize = textBlock.Width * 0.60;
			else
				textBlock.FontSize = textBlock.Height * 0.60;
		}
		private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (activeMouseLeftButtonDown)
			{
				activeMouseLeftButtonDown = false;
				bool check = WordVerification(listWords);
				if (check)
				{
					if (!gameInfo.ListOfFoundWords.Contains(foundWord))
					{
						for (int i = 0; i < listWords.Count; i++)
						{
							(canvas.FindName(listWords[i]) as TextBlock).Background = (Brush)typeof(Brushes).GetProperty(SettingsInfo.ComboBox_foundWords).GetValue(null);
						}
						gameInfo.SetScore(foundWord.Score());
						gameInfo.AddListOfFoundWords(foundWord);
						lbl_Score.Content = gameInfo.Score;

						//Все слова найдены
						if (gameInfo.ListOfWords.Count == gameInfo.ListOfFoundWords.Count)
						{
							gameInfo.NewLevel();
							canvas.Children.Clear();
							word.Clear();
							foundWord = null;
							NewGame();
						}
					}
				}
				else
				{
					ResettingTheSelection();
				}
				listWords.Clear();
			}
			else
			{
				if (!CheckingForTheFoundLetter(e))
				{
					activeMouseLeftButtonDown = true;
					brush = (e.OriginalSource as TextBlock).Background;
					(e.OriginalSource as TextBlock).Background = (Brush)typeof(Brushes).GetProperty(SettingsInfo.ComboBox_selectedCell).GetValue(null);
					listWords.Add((e.OriginalSource as TextBlock).Name);
				}

			}
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (activeMouseLeftButtonDown && !CheckingForTheFoundLetter(e))
			{
				(e.OriginalSource as TextBlock).Background = (Brush)typeof(Brushes).GetProperty(SettingsInfo.ComboBox_selectedCell).GetValue(null);
				if (!listWords.Contains((e.OriginalSource as TextBlock).Name))
				{
					listWords.Add((e.OriginalSource as TextBlock).Name);
				}
			}
		}

		private bool CheckingForTheFoundLetter(MouseEventArgs e)
		{
			foreach (Word word in gameInfo.ListOfFoundWords)
			{
				if (word.ControlName((e.OriginalSource as TextBlock).Name))
				{
					return true;
				}
			}
			return false;
		}

		private void canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			ResettingTheSelection();
		}
		private void ResettingTheSelection()
		{
			activeMouseLeftButtonDown = false;
			for (int i = 0; i < listWords.Count; i++)
			{
				(canvas.FindName(listWords[i]) as TextBlock).Background = brush;
			}
			listWords.Clear();
		}
		private bool WordVerification(List<string> word1)
		{
			foreach (Word item in word)
			{
				if (item.CheckingTheWord(word1))
				{
					foundWord = item;
					return true;
				}
			}
			return false;
		}

		private void SaveResult()
		{
			if (!checkSave)
			{
				new WorkWithFiles().WriteFile(gameInfo);
				if (resume)
					ratingInfo[ratingInfo.Count - 1] = new RatingInfo(gameInfo.NamePlayer, gameInfo.Score, gameInfo.Level);
				else
					ratingInfo.Add(new RatingInfo(gameInfo.NamePlayer, gameInfo.Score, gameInfo.Level));
				new WorkWithFiles().WriteFile(ratingInfo);
				checkSave = true;
			}

		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SaveResult();
		}
	}
}
