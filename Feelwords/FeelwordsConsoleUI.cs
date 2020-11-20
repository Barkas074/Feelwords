using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Feelwords
{
	class FeelwordsConsoleUI : TheNamesOfTheItems
	{
		int choice = 1;
		bool isChoice = true;
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

		private void SelectedKey(ConsoleKey key)
		{
			if (key == ConsoleKey.UpArrow && choice != 1)
			{
				--choice;
				isChoice = true;
			}
			else if (key == ConsoleKey.DownArrow && choice != menuOption.Length - 2)
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
				if (choice != menuOption.Length - 2)
					++choice;
				else
					choice = 1;
				isChoice = true;
			}
		}

		public void DrawingMenu(int choice)
		{
			Console.SetWindowSize(150, 42);
			Console.Clear();
			int centerX;
			int centerY = 1;
			for (int i = 0; i < menuOption.Length - 1; i++)
			{
				for (int j = 0; j < menuOption[i].Length; j++)
				{
					if (i == choice)
						Console.ForegroundColor = ConsoleColor.Yellow;
					else
						Console.ForegroundColor = ConsoleColor.Cyan;
					centerX = (Console.WindowWidth / 2) - (menuOption[i][j].Length / 2);
					Console.SetCursorPosition(centerX, centerY);
					Console.WriteLine(menuOption[i][j]);
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
			for (int i = 0; i < NAMEREQUEST.Length; i++)
			{
				centerX = (Console.WindowWidth / 2) - (NAMEREQUEST[i].Length / 2);
				Console.SetCursorPosition(centerX, centerY);
				Console.WriteLine(NAMEREQUEST[i]);
				centerY = Console.CursorTop;
			}
			Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.CursorTop);
			string name = Console.ReadLine();

		}

		public void DrawingResume()
		{
			Dummy(2);
		}

		public void DrawingRating()
		{
			Dummy(3);
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

		private void Dummy(int choice)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Clear();
			int centerX;
			int centerY = 1;
			int dummy = 5;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < menuOption[dummy].Length; j++)
				{
					centerX = (Console.WindowWidth / 2) - (menuOption[dummy][j].Length / 2);
					Console.SetCursorPosition(centerX, centerY);
					Console.WriteLine(menuOption[dummy][j]);
					centerY = Console.CursorTop;
				}
				dummy = choice;
			}
		}
	}
}
