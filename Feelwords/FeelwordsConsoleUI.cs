using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Feelwords
{
	class FeelwordsConsoleUI : TheNamesOfTheItems
	{

		public void DrawingMenu()
		{
			Console.SetWindowSize(Console.WindowWidth, 42);
			Console.Clear();
			string[][] menuOption = new string[][] { NAMEGAME, NEWGAME, RESUME, RATING, EXIT };
			int centerX;
			int centerY = 1;
			for (int i = 0; i < menuOption.Length; i++)
			{
				for (int j = 0; j < menuOption[i].Length; j++)
				{
					centerX = (Console.WindowWidth / 2) - (menuOption[i][j].Length / 2);
					Console.SetCursorPosition(centerX, centerY);
					Console.WriteLine(menuOption[i][j]);
					centerY = Console.CursorTop;
				}
			}
			Console.ReadKey();

		}
	}
}
