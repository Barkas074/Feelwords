using System;

namespace Feelwords
{
	class Program
	{
		static void Main(string[] args)
		{
			FeelwordsConsoleUI consoleUI = new FeelwordsConsoleUI();
			int choice = consoleUI.Main();
			if (choice == 1)
				consoleUI.DrawingNewGame();
			else if (choice == 2)
				consoleUI.DrawingResume();
			else if (choice == 3)
				consoleUI.DrawingRating();
			else if (choice == 4)
				Environment.Exit(0);
		}
	}
}
