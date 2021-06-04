using Feelwords.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Feelwords.WPF
{
	class GameField
	{
		private Canvas canvas { get; set; }
		private Label score { get; set; }
		public GameField(Canvas canvas, Label score)
		{
			this.canvas = canvas;
			this.score = score;
			//CreateField(canvas, CreatingDictionaryForTheGame());
			//SetScore();
		}

		private void SetScore(GameInfo levelField)
		{
			score.Content = levelField.Score;
		}

		//public void CreateField(Canvas canvas, GameInfo levelField)
		//{
		//	canvas.Children.Clear();
		//	char[,] field = levelField.Field;
		//	for (int i = 0; i < 5; i++)
		//	{
		//		for (int j = 0; j < 5; j++)
		//		{
		//			canvas.Children.Add(CreateCell(field, i, j));
		//		}
		//	}
		//}
		private Label CreateCell(char[,] field, int i, int j)
		{
			Label cell = new()
			{
				Content = field[i, j],
				HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
				VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
				Background = Brushes.Gray,
				Foreground = Brushes.Black,
				Width = canvas.Width / 5 - 10,
				Height = canvas.Height / 5 - 10
			};
			if (!double.IsNaN(cell.Width) && !double.IsNaN(cell.Height))
				SetFontSize(cell);
			Canvas.SetLeft(cell, i * canvas.Width / 5);
			Canvas.SetTop(cell, j * canvas.Height / 5);
			return cell;
		}
		private void SetFontSize(Label cell)
		{
			if (cell.Width < cell.Height)
				cell.FontSize = cell.Width * 0.60;
			else
				cell.FontSize = cell.Height * 0.60;
		}
		//private GameInfo CreatingDictionaryForTheGame()
		//{
		//	//Временная заглушка 
		//	char[,] field = new char[,] { { 'б', 'е', 'з', 'н', 'е' }, { 'а', 'п', 'о', 'т', 'н' }, { 'с', 'ь', 'а', 'б', 'о' }, { 'н', 'т', 'н', 'и', 'т' }, { 'о', 'с', 'а', 'и', 'с' } };
		//	Dictionary<string, List<string>> dictionaryWord = new Dictionary<string, List<string>>();
		//	dictionaryWord.Add("безопасность", new List<string>() { "0 0", "0 1", "0 2", "1 2", "1 1", "1 0", "2 0", "3 0", "4 0", "4 1", "3 1", "2 1" });
		//	dictionaryWord.Add("истина", new List<string>() { "4 3", "4 4", "3 4", "3 3", "3 2", "4 2" });
		//	dictionaryWord.Add("абонент", new List<string>() { "2 2", "2 3", "2 4", "1 4", "0 4", "0 3", "1 3" });
		//	GameInfo level = new LevelField(field, dictionaryWord, 0);
		//	return level;
		//}
	}
}
