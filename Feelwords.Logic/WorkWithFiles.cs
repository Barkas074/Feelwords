namespace Feelwords.Logic
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	public class WorkWithFiles
	{
		private const string settingsFile = "settings.txt";
		private const string ratingFile = "records.txt";
		private const string gameInfoFile = "gameInfo.txt";

		public void WriteFile(object obj)
		{
			if (obj is GameInfo)
			{
				WriteTextToFile(gameInfoFile, JsonConvert.SerializeObject(obj));
			}
			if (obj is List<RatingInfo>)
			{
				WriteTextToFile(ratingFile, JsonConvert.SerializeObject(obj));
			}
			if (obj is SettingsInfoJSON)
			{
				WriteTextToFile(settingsFile, JsonConvert.SerializeObject(obj));
			}
		}
		public void ReadFiles(out List<RatingInfo> ratingInfo, out GameInfo gameInfo)
		{
			ratingInfo = new();
			gameInfo = null;
			SettingsInfoJSON settingsInfoJSON;
			if (File.Exists(settingsFile))
			{
				settingsInfoJSON = JsonConvert.DeserializeObject<SettingsInfoJSON>(File.ReadAllText(settingsFile));
				SettingsInfo.SetSettingsInfo(settingsInfoJSON.ComboBox_selectedCell, settingsInfoJSON.ComboBox_foundWords, settingsInfoJSON.ComboBox_hint, settingsInfoJSON.ComboBox_fontColor, settingsInfoJSON.ComboBox_fontColorFoundWords);
			}
			if (File.Exists(ratingFile))
			{
				foreach (RatingInfo item in JsonConvert.DeserializeObject<List<RatingInfo>>(File.ReadAllText(ratingFile)))
				{
					ratingInfo.Add(item);
				}
			}
			if (File.Exists(gameInfoFile))
			{
				gameInfo = JsonConvert.DeserializeObject<GameInfo>(File.ReadAllText(gameInfoFile));
			}
		}

		private void WriteTextToFile(string nameFile, string text)
		{
			using FileStream fStream = new FileStream(nameFile, FileMode.Create);
			byte[] array = Encoding.Default.GetBytes(text);
			fStream.Write(array, 0, array.Length);
		}

		private string[] ReadTextFromFile(string nameFile)
		{
			return File.ReadAllLines(nameFile);
		}

		public List<string> ReadWordsFromFile(List<int> countWord)
		{
			List<string> listWords = new List<string>();
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			if (!File.Exists("words.txt"))
			{
				WriteTextToFile("words.txt", Properties.Resources.words);
			}
			string[] words = File.ReadAllLines("words.txt");
			Random rd = new Random();
			for (int i = 0; i < countWord.Count; i++)
			{
				int number;
				do
				{
					number = rd.Next(0, words.Length);
				}
				while (words[number].Length != countWord[i]);
				listWords.Add(words[number]);
			}
			return listWords;
		}

		public void WorkWithFilesOld(string name, long gamePoints)
		{
			string[] text;
			string[] newText = new string[10];
			if (!File.Exists(settingsFile))
				WriteTextToFile(settingsFile, string.Empty);
			if (name != string.Empty)
			{
				if (!File.Exists(ratingFile))
				{
					WriteTextToFile(ratingFile, name + ": " + gamePoints);
				}
				else
				{
					text = ReadTextFromFile(ratingFile);
					string[] temp;
					bool checkName = false;
					for (int i = 0; i < text.Length; i++)
					{
						if (text[i] != string.Empty)
						{
							temp = text[i].Split(": ", StringSplitOptions.RemoveEmptyEntries);
							if (temp[0] == name)
							{
								text[i] = name + ": " + (long.Parse(temp[1]) + gamePoints);
								checkName = true;
								WriteTextToFile(ratingFile, string.Join("\n", text));
								break;
							}
						}
					}
					if (!checkName)
					{
						bool check = false;
						for (int i = 0; i < text.Length; i++)
						{
							if (text[i] != string.Empty)
							{
								temp = text[i].Split(": ", StringSplitOptions.RemoveEmptyEntries);
								if (gamePoints >= long.Parse(temp[1]))
								{
									newText[i] = name + ": " + gamePoints;
									newText[i + 1] = text[i];
									check = true;
								}
								else
								{
									if (check)
									{
										newText[i + 1] = text[i];
									}
									else
									{
										newText[i] = text[i];
									}
								}
							}
						}
						if (newText == null)
						{
							newText[0] = string.Empty;
						}
						WriteTextToFile(ratingFile, string.Join("\n", newText));
					}
				}
			}
		}
	}
}
