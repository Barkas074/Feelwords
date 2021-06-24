using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feelwords.Logic
{
	public class RatingInfo
	{
		public string NamePlayer { get; private set; }
		public int Score { get; private set; } = 0;
		public string Level { get; private set; } = "Уровень 1";

		public RatingInfo(string namePlayer, int score, int level)
		{
			NamePlayer = namePlayer;
			Score = score;
			Level = level != 0 ? "Уровень " + level : "Польз.";
		}
		[JsonConstructor]
		public RatingInfo(string namePlayer, int score, string level)
		{
			NamePlayer = namePlayer;
			Score = score;
			Level = level;
		}
	}
}
