using System;
using System.IO;
using System.Text;

namespace Feelwords
{
	class Program
	{
		static void Main(string[] args)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			string[] dictionary = File.ReadAllLines("input.txt", Encoding.GetEncoding(1251));
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
