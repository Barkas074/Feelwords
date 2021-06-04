namespace Feelwords.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class GameInfo
	{
		public int Width; 
		public int Height; 
		public List<Word> DictionaryWord;
		public string NamePlayer;
		public int Score;

		public GameInfo(int width, int height, string namePlayer, int score)
		{
			Width = width;
			Height = height;
			NamePlayer = namePlayer;
			Score = score;
		}

		public void SetScore(int score) 
		{
			Score += score;
		}
	}
}
