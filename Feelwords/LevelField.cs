using System;
using System.Collections.Generic;
using System.Text;

namespace Feelwords
{
	class LevelField
	{
		public char[,] field;
		public Dictionary<string, List<string>> dictionaryWord;

		public LevelField(char[,] field, Dictionary<string, List<string>> dictionaryWord)
		{
			this.field = field;
			this.dictionaryWord = dictionaryWord;
		}
	}
}
