namespace Feelwords.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class GameInfo
	{
		public int FieldWidth { get; private set; } = 5;
		public int FieldHeight { get; private set; } = 5;
		public List<Word> ListOfWords { get; set; }
		public List<Word> ListOfFoundWords { get; private set; } = new List<Word>();
		public string NamePlayer { get; private set; }
		public int Score { get; private set; }
		public int Level { get; private set; } = 1;

		public GameInfo(int fieldWidth, int fieldHeight, string namePlayer, int score, int level = 1)
		{
			FieldWidth = fieldWidth;
			FieldHeight = fieldHeight;
			NamePlayer = namePlayer;
			Score = score;
			Level = level;
		}

		public void SetScore(int score)
		{
			Score += score;
		}

		public void AddListOfFoundWords(Word foundWord)
		{
			ListOfFoundWords.Add(foundWord);
		}
		public void NewLevel()
		{
			if (Level != 0)
				Level++;
			FieldWidth++;
			FieldHeight++;
			ListOfWords.Clear();
			ListOfFoundWords.Clear();
		}
	}
}
