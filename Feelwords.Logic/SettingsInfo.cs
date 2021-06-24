using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Feelwords.Logic
{
	public static class SettingsInfo
	{
		public static string ComboBox_selectedCell { get; private set; } = "Cyan";
		public static string ComboBox_foundWords { get; private set; } = "Green";
		public static string ComboBox_hint { get; private set; } = "Yellow";
		public static string ComboBox_fontColor { get; private set; } = "Black";
		public static string ComboBox_fontColorFoundWords { get; private set; } = "Black";

		public static void SetSettingsInfo(string comboBox_selectedCell, string comboBox_foundWords, string comboBox_hint, string comboBox_fontColor, string comboBox_fontColorFoundWords)
		{
			ComboBox_selectedCell = comboBox_selectedCell;
			ComboBox_foundWords = comboBox_foundWords;
			ComboBox_hint = comboBox_hint;
			ComboBox_fontColor = comboBox_fontColor;
			ComboBox_fontColorFoundWords = comboBox_fontColorFoundWords;
		}
		public static void ResetSettingsInfo()
		{
			ComboBox_selectedCell = "Cyan";
			ComboBox_foundWords = "Green";
			ComboBox_hint = "Yellow";
			ComboBox_fontColor = "Black";
			ComboBox_fontColorFoundWords = "Black";
		}
	}
	public class SettingsInfoJSON
	{
		public string ComboBox_selectedCell { get; private set; }
		public string ComboBox_foundWords { get; private set; }
		public string ComboBox_hint { get; private set; }
		public string ComboBox_fontColor { get; private set; }
		public string ComboBox_fontColorFoundWords { get; private set; }

		public SettingsInfoJSON(string comboBox_selectedCell, string comboBox_foundWords, string comboBox_hint, string comboBox_fontColor, string comboBox_fontColorFoundWords)
		{
			ComboBox_selectedCell = comboBox_selectedCell;
			ComboBox_foundWords = comboBox_foundWords;
			ComboBox_hint = comboBox_hint;
			ComboBox_fontColor = comboBox_fontColor;
			ComboBox_fontColorFoundWords = comboBox_fontColorFoundWords;
		}
	}
}
