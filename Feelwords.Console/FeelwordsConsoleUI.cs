﻿namespace Feelwords.Console
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Text;
	using Feelwords.Logic;

	class FeelwordsConsoleUI
	{
		int choice = 1;
		bool isChoice = true;
		TheNamesOfTheItems theNamesOfTheItems = new TheNamesOfTheItems();
		public int Main()
		{
			Console.CursorVisible = false;
			while (true)
			{
				if (isChoice)
					DrawingMenu(choice);
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.Tab:
						SelectedKey(ConsoleKey.Tab);
						break;
					case ConsoleKey.Enter:
						return choice;
					case ConsoleKey.Escape:
						Environment.Exit(0);
						break;
					case ConsoleKey.Spacebar:
						return choice;
					case ConsoleKey.UpArrow:
						SelectedKey(ConsoleKey.UpArrow);
						break;
					case ConsoleKey.DownArrow:
						SelectedKey(ConsoleKey.DownArrow);
						break;
					case ConsoleKey.S:
						SelectedKey(ConsoleKey.DownArrow);
						break;
					case ConsoleKey.W:
						SelectedKey(ConsoleKey.UpArrow);
						break;
					default:
						isChoice = false;
						break;
				}
			}
		}

		private string[] ReadingFile()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			if (!File.Exists("dictionary.txt"))
			{
				File.WriteAllText("dictionary.txt", "абажур\nабордаж\nаборт\nабонент\nавиалиния\nавтобаза\nавтограф\nавтомобилист\nавтор\nбал\nбалет\nбалкон\nбалерина\nбандит\nбаня\nбанкомат\nбарьер\nбасня\nбассейн\nбездна\nбезопасность\nбелка\nберег\nберет\nигрок\nидеал\nигра\nидол\nидея\nизобретатель\nизумруд\nикона\nил\nйод\nион\nиск\nистина\nитог\nум\nяк");
			}
			return File.ReadAllLines("dictionary.txt", Encoding.GetEncoding(1251));
		}

		private long PrepareField(int width, int height)
		{
			long gamePoints = 0;
			string[] dictionary = ReadingFile();
			for (int i = 1; i <= 10; i++)
			{
				DrawingField(width, height);
				GameInfo level = CreatingDictionaryForTheGame(width, height);
				FillField(width, height, level, dictionary);
				gamePoints += StartTheGame(width, height, level, dictionary, out bool keyEsc);
				if (keyEsc)
				{
					break;
				}
			}
			return gamePoints;
		}

		private GameInfo CreatingDictionaryForTheGame(int width, int height)
		{
			//Временная заглушка 
			char[,] field = new char[,] { { 'б', 'е', 'з', 'н', 'е' }, { 'а', 'п', 'о', 'т', 'н' }, { 'с', 'ь', 'а', 'б', 'о' }, { 'н', 'т', 'н', 'и', 'т' }, { 'о', 'с', 'а', 'и', 'с' } };
			Dictionary<string, List<string>> dictionaryWord = new Dictionary<string, List<string>>();
			dictionaryWord.Add("безопасность", new List<string>() { "0 0", "0 1", "0 2", "1 2", "1 1", "1 0", "2 0", "3 0", "4 0", "4 1", "3 1", "2 1" });
			dictionaryWord.Add("истина", new List<string>() { "4 3", "4 4", "3 4", "3 3", "3 2", "4 2" });
			dictionaryWord.Add("абонент", new List<string>() { "2 2", "2 3", "2 4", "1 4", "0 4", "0 3", "1 3" });
			GameInfo level = new LevelField(field, dictionaryWord, 0);
			return level;
		}

		private void SelectedKey(ConsoleKey key)
		{
			if (key == ConsoleKey.UpArrow && choice != 1)
			{
				--choice;
				isChoice = true;
			}
			else if (key == ConsoleKey.DownArrow && choice != theNamesOfTheItems.menuOption.Length - 2)
			{
				++choice;
				isChoice = true;
			}
			else
			{
				isChoice = false;
			}
			if (key == ConsoleKey.Tab)
			{
				if (choice != theNamesOfTheItems.menuOption.Length - 2)
					++choice;
				else
					choice = 1;
				isChoice = true;
			}
		}

		public void DrawingMenu(int choice)
		{
			Console.SetWindowSize(150, 52);
			Console.Clear();
			int centerX;
			int centerY = 1;
			for (int i = 0; i < theNamesOfTheItems.menuOption.Length - 1; i++)
			{
				for (int j = 0; j < theNamesOfTheItems.menuOption[i].Length; j++)
				{
					if (i == choice)
						Console.ForegroundColor = ConsoleColor.Yellow;
					else
						Console.ForegroundColor = ConsoleColor.Cyan;
					centerX = Console.WindowWidth / 2 - theNamesOfTheItems.menuOption[i][j].Length / 2;
					Console.SetCursorPosition(centerX, centerY);
					Console.WriteLine(theNamesOfTheItems.menuOption[i][j]);
					centerY = Console.CursorTop;
				}
			}
		}

		public void DrawingNewGame()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Clear();
			int centerX;
			int centerY = 1;
			for (int i = 0; i < theNamesOfTheItems.NAMEREQUEST.Length; i++)
			{
				centerX = Console.WindowWidth / 2 - theNamesOfTheItems.NAMEREQUEST[i].Length / 2;
				Console.SetCursorPosition(centerX, centerY);
				Console.WriteLine(theNamesOfTheItems.NAMEREQUEST[i]);
				centerY = Console.CursorTop;
			}
			Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.CursorTop);
			string name = Console.ReadLine();
			long gamePoints = PrepareField(5, 5); //Временная заглушка на размер поля
			WorkWithFiles workWithFiles = new();
			workWithFiles.WorkWithFilesOld(name, gamePoints);
		}



		public void DrawingResume()
		{
			Dummy(2);
			while (true)
			{
				if (Console.ReadKey(true).Key == ConsoleKey.Escape)
				{
					break;
				}
			}
		}

		public void DrawingRating()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Clear();
			if (File.Exists("records.txt"))
			{
				string[] text = File.ReadAllLines("records.txt");
				foreach (var line in text)
				{
					Console.WriteLine(line);
				}
			}
			while (true)
			{
				if (Console.ReadKey(true).Key == ConsoleKey.Escape)
				{
					break;
				}
			}
		}

		public void DrawingSettings()
		{
			Dummy(4);
			while (true)
			{
				if (Console.ReadKey(true).Key == ConsoleKey.Escape)
				{
					break;
				}
			}
		}

		private void DrawingField(int fieldWidth, int fieldHeight)
		{
			Console.Clear();
			for (int i = 0; i < fieldHeight; i++)
			{
				string topLeftCorner = "╔";
				string topRightCorner = "╗";
				string lowerLeftCorner = "╚";
				string lowerRightCorner = "╝";
				for (int j = 0; j < fieldWidth; j++)
				{
					Console.SetCursorPosition(j * 2, i * 2);
					if (i != 0)
					{
						topLeftCorner = "╠";
						topRightCorner = "╣";
					}
					if (j != 0)
					{
						topLeftCorner = "╦";
						lowerLeftCorner = "╩";
					}
					if (i != 0 && j != 0)
					{
						topLeftCorner = "╬";
					}
					Console.WriteLine(topLeftCorner + "═" + topRightCorner);
					Console.SetCursorPosition(j * 2, Console.CursorTop);
					Console.WriteLine("║" + " " + "║");
					Console.SetCursorPosition(j * 2, Console.CursorTop);
					Console.Write(lowerLeftCorner + "═" + lowerRightCorner);
				}
			}
		}

		private void FillField(int fieldWidth, int fieldHeight, GameInfo level, string[] dictionary)
		{
			int cursorPositionY = 1;
			for (int i = 0; i < fieldHeight; i++)
			{
				int cursorPositionX = 1;
				for (int j = 0; j < fieldWidth; j++)
				{
					Console.SetCursorPosition(cursorPositionX, cursorPositionY);
					Console.Write(level.Field[i, j]);
					cursorPositionX += 2;
				}
				cursorPositionY += 2;
			}
		}

		private long StartTheGame(int fieldWidth, int fieldHeight, GameInfo level, string[] dictionary, out bool keyEsc)
		{
			long gamePoints = 0;
			Console.SetCursorPosition(15, 0);
			Console.Write("Кол-во очков: " + gamePoints);
			int cursorPositionX = 1;
			int cursorPositionY = 1;
			int cellPositionX = 0;
			int cellPositionY = 0;
			bool keyEnter = false;
			keyEsc = true;
			Stack<string> cursorPosition = new Stack<string>();
			Stack<string> cellPosition = new Stack<string>();
			Dictionary<string, List<string>> guessTheWord = new Dictionary<string, List<string>>();
			while (true)
			{
				bool checkLetter = false;
				foreach (var word in guessTheWord)
				{
					int j = 0;
					for (int i = word.Value.Count - 1; i >= 0; i--)
					{
						if (word.Value[j] == cellPositionX + " " + cellPositionY)
						{
							Console.BackgroundColor = ConsoleColor.Green;
							Console.ForegroundColor = ConsoleColor.Gray;
							checkLetter = true;
							break;
						}
						j++;
					}
					if (checkLetter)
					{
						break;
					}
				}
				if (!checkLetter)
				{
					Console.BackgroundColor = ConsoleColor.Red;
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				Console.SetCursorPosition(cursorPositionY, cursorPositionX);
				Console.Write(level.Field[cellPositionX, cellPositionY]);
				//if (isChoice)
				//	DrawingMenu(choice);
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.Enter:
						if (!keyEnter)
						{
							keyEsc = false;
							keyEnter = true;
							cursorPosition.Push(cursorPositionY + " " + cursorPositionX);
							cellPosition.Push(cellPositionX + " " + cellPositionY);
							break;
						}
						else
						{
							keyEsc = false;
							keyEnter = false;
							if (WordVerification(level, cellPosition, dictionary, ref guessTheWord, ref gamePoints))
							{
								SelectionOfTheCells(ConsoleColor.Green, ConsoleColor.Gray, level, ref cursorPosition, ref cellPosition);
								if (guessTheWord.Count == level.DictionaryWord.Count)
								{
									Console.BackgroundColor = ConsoleColor.Black;
									Console.ForegroundColor = ConsoleColor.Cyan;
									gamePoints += 500;
									return gamePoints;
								}
							}
							else
							{
								SelectionOfTheCells(ConsoleColor.Black, ConsoleColor.Cyan, level, ref cursorPosition, ref cellPosition);
							}
							break;
						}
					case ConsoleKey.Escape:
						if (!keyEsc)
						{
							SelectedKeyInTheGame(ConsoleKey.Escape, fieldWidth, fieldHeight, ref cursorPositionX, ref cursorPositionY, ref cellPositionX, ref cellPositionY, level, ref keyEnter, ref cursorPosition, ref cellPosition, checkLetter);
							keyEsc = true;
							break;
						}
						else
						{
							Console.BackgroundColor = ConsoleColor.Black;
							Console.ForegroundColor = ConsoleColor.Cyan;
							return 0;
						}
					case ConsoleKey.UpArrow:
						SelectedKeyInTheGame(ConsoleKey.UpArrow, fieldWidth, fieldHeight, ref cursorPositionX, ref cursorPositionY, ref cellPositionX, ref cellPositionY, level, ref keyEnter, ref cursorPosition, ref cellPosition, checkLetter);
						keyEsc = false;
						break;
					case ConsoleKey.DownArrow:
						SelectedKeyInTheGame(ConsoleKey.DownArrow, fieldWidth, fieldHeight, ref cursorPositionX, ref cursorPositionY, ref cellPositionX, ref cellPositionY, level, ref keyEnter, ref cursorPosition, ref cellPosition, checkLetter);
						keyEsc = false;
						break;
					case ConsoleKey.LeftArrow:
						SelectedKeyInTheGame(ConsoleKey.LeftArrow, fieldWidth, fieldHeight, ref cursorPositionX, ref cursorPositionY, ref cellPositionX, ref cellPositionY, level, ref keyEnter, ref cursorPosition, ref cellPosition, checkLetter);
						keyEsc = false;
						break;
					case ConsoleKey.RightArrow:
						SelectedKeyInTheGame(ConsoleKey.RightArrow, fieldWidth, fieldHeight, ref cursorPositionX, ref cursorPositionY, ref cellPositionX, ref cellPositionY, level, ref keyEnter, ref cursorPosition, ref cellPosition, checkLetter);
						keyEsc = false;
						break;
					case ConsoleKey.W:
						SelectedKeyInTheGame(ConsoleKey.UpArrow, fieldWidth, fieldHeight, ref cursorPositionX, ref cursorPositionY, ref cellPositionX, ref cellPositionY, level, ref keyEnter, ref cursorPosition, ref cellPosition, checkLetter);
						keyEsc = false;
						break;
					case ConsoleKey.S:
						SelectedKeyInTheGame(ConsoleKey.DownArrow, fieldWidth, fieldHeight, ref cursorPositionX, ref cursorPositionY, ref cellPositionX, ref cellPositionY, level, ref keyEnter, ref cursorPosition, ref cellPosition, checkLetter);
						keyEsc = false;
						break;
					case ConsoleKey.A:
						SelectedKeyInTheGame(ConsoleKey.LeftArrow, fieldWidth, fieldHeight, ref cursorPositionX, ref cursorPositionY, ref cellPositionX, ref cellPositionY, level, ref keyEnter, ref cursorPosition, ref cellPosition, checkLetter);
						keyEsc = false;
						break;
					case ConsoleKey.D:
						SelectedKeyInTheGame(ConsoleKey.RightArrow, fieldWidth, fieldHeight, ref cursorPositionX, ref cursorPositionY, ref cellPositionX, ref cellPositionY, level, ref keyEnter, ref cursorPosition, ref cellPosition, checkLetter);
						keyEsc = false;
						break;
				}
			}
		}

		private void SelectionOfTheCells(ConsoleColor backgroundColor, ConsoleColor foregroundColor, GameInfo level, ref Stack<string> cursorPosition, ref Stack<string> cellPosition)
		{
			Console.BackgroundColor = backgroundColor;
			Console.ForegroundColor = foregroundColor;
			while (cursorPosition.Count != 0)
			{
				string temp1 = cursorPosition.Pop();
				string[] cursor = temp1.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				string temp2 = cellPosition.Pop();
				string[] cell = temp2.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				Console.SetCursorPosition(int.Parse(cursor[0]), int.Parse(cursor[1]));
				Console.Write(level.Field[int.Parse(cell[0]), int.Parse(cell[1])]);
			}
		}

		private bool WordVerification(GameInfo level, Stack<string> cellPosition, string[] dictionary, ref Dictionary<string, List<string>> guessTheWord, ref long gamePoints)
		{
			bool checkWord = false;
			string[] cell = cellPosition.ToArray();
			foreach (var word in level.DictionaryWord)
			{
				if (word.Value.Count == cell.Length)
				{
					int j = 0;
					for (int i = word.Value.Count - 1; i >= 0; i--)
					{
						if (word.Value[j] == cell[i])
						{
							checkWord = true;
						}
						else
						{
							checkWord = false;
							break;
						}
						j++;
					}
					if (checkWord)
					{
						if (!guessTheWord.ContainsKey(word.Key))
						{
							guessTheWord.Add(word.Key, word.Value);
							gamePoints += 100;
							Console.BackgroundColor = ConsoleColor.Black;
							Console.ForegroundColor = ConsoleColor.Cyan;
							Console.SetCursorPosition(15, 0);
							Console.Write("Кол-во очков: " + gamePoints);
						}
					}
					break;
				}
			}
			return checkWord;
		}

		private void SelectedKeyInTheGame(ConsoleKey key, int fieldWidth, int fieldHeight, ref int cursorPositionX, ref int cursorPositionY, ref int cellPositionX, ref int cellPositionY, GameInfo level, ref bool keyEnter, ref Stack<string> cursorPosition, ref Stack<string> cellPosition, bool checkLetter)
		{
			if (!keyEnter && !checkLetter)
			{
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.SetCursorPosition(cursorPositionY, cursorPositionX);
				Console.Write(level.Field[cellPositionX, cellPositionY]);
			}
			if (key == ConsoleKey.UpArrow && cellPositionX != 0)
			{
				cursorPositionX -= 2;
				cellPositionX -= 1;
			}
			else if (key == ConsoleKey.DownArrow && cellPositionX != fieldHeight - 1)
			{
				cursorPositionX += 2;
				cellPositionX += 1;
			}
			else if (key == ConsoleKey.LeftArrow && cellPositionY != 0)
			{
				cursorPositionY -= 2;
				cellPositionY -= 1;
			}
			else if (key == ConsoleKey.RightArrow && cellPositionY != fieldWidth - 1)
			{
				cursorPositionY += 2;
				cellPositionY += 1;
			}
			else if (key == ConsoleKey.Escape)
			{
				SelectionOfTheCells(ConsoleColor.Black, ConsoleColor.Cyan, level, ref cursorPosition, ref cellPosition);
				keyEnter = false;
			}
			if (keyEnter)
			{
				cursorPosition.Push(cursorPositionY + " " + cursorPositionX);
				cellPosition.Push(cellPositionX + " " + cellPositionY);
			}
		}

		private void Dummy(int choice)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Clear();
			int centerX;
			int centerY = 1;
			int dummy = 6;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < theNamesOfTheItems.menuOption[dummy].Length; j++)
				{
					centerX = Console.WindowWidth / 2 - theNamesOfTheItems.menuOption[dummy][j].Length / 2;
					Console.SetCursorPosition(centerX, centerY);
					Console.WriteLine(theNamesOfTheItems.menuOption[dummy][j]);
					centerY = Console.CursorTop;
				}
				dummy = choice;
			}
		}
	}
}
