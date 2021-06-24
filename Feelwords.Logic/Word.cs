using System;
using System.Collections.Generic;
using System.Text;

namespace Feelwords.Logic
{
	public class Word
	{
		public List<int> X { get; set; } = new List<int>();
		public List<int> Y { get; set; } = new List<int>();
		public string Name { get; set; }
		public char[] CharName { get; set; }
		public bool HintUsed { get; set; } = false;

		public string Letter(int x, int y)
		{
			for (int i = 0; i < CharName.Length; i++)
			{
				if (X[i] == x && Y[i] == y)
				{
					return CharName[i].ToString();
				}
			}
			return "Координаты не верные";
		}
		public bool ControlName(string name)
		{
			for (int i = 0; i < CharName.Length; i++)
			{
				if (name == ("tb" + X[i] + Y[i]))
				{
					return true;
				}
			}
			return false;
		}
		public bool CheckingTheWord(List<string> word)
		{
			if (CharName.Length != word.Count)
				return false;
			for (int i = 0; i < word.Count; i++)
			{
				if (word[i] != ("tb" + X[i] + Y[i]))
				{
					return false;
				}
			}
			return true;
		}
		public int Score()
		{
			return HintUsed ? 50 : 100;
		}

	}
}
 