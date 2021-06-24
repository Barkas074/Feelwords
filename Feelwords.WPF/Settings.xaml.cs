using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Feelwords.Logic;

namespace Feelwords.WPF
{
	/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : Window
	{
		private string _comboBox_selectedCell;
		private string _comboBox_foundWords;
		private string _comboBox_hint;
		private string _comboBox_fontColor;
		private string _comboBox_fontColorFoundWords;
		public Settings()
		{
			InitializeComponent();
			//Заполнение выдвижных элементов
			comboBox_selectedCell.ItemsSource = typeof(Brushes).GetProperties();
			comboBox_foundWords.ItemsSource = typeof(Brushes).GetProperties();
			comboBox_hint.ItemsSource = typeof(Brushes).GetProperties();
			comboBox_fontColor.ItemsSource = typeof(Brushes).GetProperties();
			comboBox_fontColorFoundWords.ItemsSource = typeof(Brushes).GetProperties();

			getData();
			SelectedValues();
		}
		private void getData()
		{
			_comboBox_selectedCell = SettingsInfo.ComboBox_selectedCell;
			_comboBox_foundWords = SettingsInfo.ComboBox_foundWords;
			_comboBox_hint = SettingsInfo.ComboBox_hint;
			_comboBox_fontColor = SettingsInfo.ComboBox_fontColor;
			_comboBox_fontColorFoundWords = SettingsInfo.ComboBox_fontColorFoundWords;
		}
		private void SelectedValues()
		{
			comboBox_selectedCell.SelectedItem = typeof(Brushes).GetProperty(_comboBox_selectedCell);
			comboBox_foundWords.SelectedItem = typeof(Brushes).GetProperty(_comboBox_foundWords);
			comboBox_hint.SelectedItem = typeof(Brushes).GetProperty(_comboBox_hint);
			comboBox_fontColor.SelectedItem = typeof(Brushes).GetProperty(_comboBox_fontColor);
			comboBox_fontColorFoundWords.SelectedItem = typeof(Brushes).GetProperty(_comboBox_fontColorFoundWords);
		}

		private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//Так и не додумался как лучше расписать этот участок кода.
			switch (((FrameworkElement)e.OriginalSource).Name)
			{
				case "comboBox_selectedCell":
					_comboBox_selectedCell = ((sender as ComboBox).SelectedItem as System.Reflection.PropertyInfo).Name;
					break;
				case "comboBox_foundWords":
					_comboBox_foundWords = ((sender as ComboBox).SelectedItem as System.Reflection.PropertyInfo).Name;
					break;
				case "comboBox_hint":
					_comboBox_hint = ((sender as ComboBox).SelectedItem as System.Reflection.PropertyInfo).Name;
					break;
				case "comboBox_fontColor":
					_comboBox_fontColor = ((sender as ComboBox).SelectedItem as System.Reflection.PropertyInfo).Name;
					break;
				case "comboBox_fontColorFoundWords":
					_comboBox_fontColorFoundWords = ((sender as ComboBox).SelectedItem as System.Reflection.PropertyInfo).Name;
					break;
				default:
					break;
			}
			
		}
		private void btn_Back_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new();
			mainWindow.Show();
			Close();
		}
		private void btn_Default_Click(object sender, RoutedEventArgs e)
		{
			SettingsInfo.ResetSettingsInfo();
			getData();
			SelectedValues();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SettingsInfo.SetSettingsInfo(_comboBox_selectedCell, _comboBox_foundWords, _comboBox_hint, _comboBox_fontColor, _comboBox_fontColorFoundWords);
			new WorkWithFiles().WriteFile(new SettingsInfoJSON(_comboBox_selectedCell, _comboBox_foundWords, _comboBox_hint, _comboBox_fontColor, _comboBox_fontColorFoundWords));
		}
	}
}
