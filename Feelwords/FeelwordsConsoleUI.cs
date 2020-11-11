using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Feelwords
{
	class FeelwordsConsoleUI : TheNamesOfTheItems
	{
		public int Main()
		{
			Console.CursorVisible = false;
			int choice = 1;
			bool isChoice = true;
			while (true)
			{
				if (isChoice)
					DrawingMenu(choice);
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.Tab:
						if (choice != menuOption.Length - 1)
							++choice;
						else
							choice = 1;
						isChoice = true;
						break;
					case ConsoleKey.Enter:
						Console.CursorVisible = true;
						return choice;
					case ConsoleKey.Escape:
						Environment.Exit(0);
						break;
					case ConsoleKey.Spacebar:
						Console.CursorVisible = true;
						return choice;
					case ConsoleKey.UpArrow:
						if (choice != 1)
						{
							--choice;
							isChoice = true;
						}
						else
						{
							isChoice = false;
						}
						break;
					case ConsoleKey.DownArrow:
						if (choice != menuOption.Length - 1)
						{
							++choice;
							isChoice = true;
						}
						else
						{
							isChoice = false;
						}
						break;
					case ConsoleKey.S:
						if (choice != menuOption.Length - 1)
						{
							++choice;
							isChoice = true;
						}
						else
						{
							isChoice = false;
						}
						break;
					case ConsoleKey.W:
						if (choice != 1)
						{
							--choice;
							isChoice = true;
						}
						else
						{
							isChoice = false;
						}
						break;
					default:
						isChoice = false;
						break;
				}
			}
		}

		public void DrawingMenu(int choice)
		{
			Console.SetWindowSize(Console.WindowWidth, 42);
			Console.Clear();
			int centerX;
			int centerY = 1;
			for (int i = 0; i < menuOption.Length; i++)
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
	}
}
