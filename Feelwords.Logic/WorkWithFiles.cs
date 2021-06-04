namespace Feelwords.Logic
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	public class WorkWithFiles
	{
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
			string[] words = File.ReadAllLines("words.txt", Encoding.GetEncoding(1251));
			for (int i = 0; i < countWord.Count; i++)
			{
				foreach (var item in words)
				{
					if (item.Length == countWord[i])
					{
						listWords.Add(item);
						break;
					}
				}
			}
			return listWords;
		}

		public WorkWithFiles(string name, long gamePoints)
		{
			string configFile = "config.txt";
			string recordsFile = "records.txt";
			string[] text;
			string[] newText = new string[10];
			if (!File.Exists(configFile))
				WriteTextToFile(configFile, string.Empty);
			if (name != string.Empty)
			{
				if (!File.Exists(recordsFile))
				{
					WriteTextToFile(recordsFile, name + ": " + gamePoints);
				}
				else
				{
					text = ReadTextFromFile(recordsFile);
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
								WriteTextToFile(recordsFile, string.Join("\n", text));
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
						WriteTextToFile(recordsFile, string.Join("\n", newText));
					}
				}
			}
		}
	}
}
