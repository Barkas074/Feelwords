using System;
using System.Collections.Generic;
using System.Text;

namespace Feelwords.Logic
{
	public class NewGame
	{
		public GameInfo LevelField { get; private set; }
		//public List<Word> Words { get; private set; } = new List<Word>(); //Угаданные слова
		public int Score { get; set; }
		public NewGame(GameInfo levelField)
		{
			LevelField = levelField;
			Score = levelField.Score;
		}
		//public bool CheckWord(Word word)
		//{
		//	for (int i = 0; i < GameTable.Words.Count; i++)
		//	{
		//		if (GameTable.Words[i].CoordsX.SequenceEqual(word.X) && GameTable.Words[i].CoordsY.SequenceEqual(word.Y))
		//		{
		//			Words.Add(GetWord(word));
		//			GameTable.Words.Remove(GameTable.Words[i]);
		//			return true;
		//		}
		//	}
		//	return false;
		//}
		//public static bool CheckInWords(int x, int y, NewGame game)
		//{
		//	for (int i = 0; i < game.Words.Count; i++)
		//	{
		//		for (int k = 0; k < game.Words[i].X.Count; k++)
		//		{
		//			if (game.Words[i].X[k] == x && game.Words[i].Y[k] == y)
		//				return true;
		//		}
		//	}
		//	return false;
		//}
		//public static bool CheckLetter(int x, int y, Word SelectedWord)
		//{
		//	for (int i = 0; i < SelectedWord.X.Count; i++)
		//	{
		//		if (SelectedWord.X[i] == x && SelectedWord.Y[i] == y)
		//			return true;
		//	}
		//	return false;
		//}
		//Word GetWord(Word word)
		//{
		//	Word word1 = new Word();
		//	int[] coordsX = word.X.ToArray();
		//	int[] coordsY = word.Y.ToArray();
		//	for (int k = 0; k < coordsX.Length; k++)
		//	{
		//		word1.X.Add(coordsX[k]);
		//		word1.Y.Add(coordsY[k]);
		//	}
		//	return word1;
		//}
		//public bool GetNextLvl()
		//{
		//	if (Score == 0)
		//	{
		//		Gamer.SetTable(GameTable.CreateTable());
		//		Gamer.SetScores(ScoresForLvl);
		//		ScoresForLvl = GameTable.Words.Count;
		//		Words.Clear();
		//		return true;
		//	}
		//	return false;
		//}
	}
}
