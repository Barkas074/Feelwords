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
	/// Interaction logic for UserDefinition.xaml
	/// </summary>
	public partial class UserDefinition : Window
	{
		List<RatingInfo> ratingInfo;
		bool checkName;
		bool enableCustomField;
		bool checkX;
		bool checkY;
		public UserDefinition(List<RatingInfo> ratingInfo)
		{
			InitializeComponent();
			this.ratingInfo = ratingInfo;
		}

		private void btn_Back_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new();
			mainWindow.Show();
			Close();
		}

		private void btn_NewGame_Click(object sender, RoutedEventArgs e)
		{
			if (!int.TryParse(tbox_X.Text, out int fieldWidth))
				fieldWidth = 5;
			if (!int.TryParse(tbox_Y.Text, out int fieldHeight))
				fieldHeight = 5;
			Game game = new(tbox_PlayerName, ratingInfo, enableCustomField, fieldWidth, fieldHeight);
			game.Show();
			Close();
		}

		private void tbox_PlayerName_TextChanged(object sender, TextChangedEventArgs e)
		{
			CheckTextBox(sender);
		}

		private void customSettings_Click(object sender, RoutedEventArgs e)
		{
			customField.Visibility = Visibility.Collapsed;
			tbox_X.Visibility = Visibility.Visible;
			tbox_colon.Visibility = Visibility.Visible;
			tbox_Y.Visibility = Visibility.Visible;
			btn_NewGame.IsEnabled = false;
			enableCustomField = true;
		}
		private void tbox_customField_TextChanged(object sender, TextChangedEventArgs e)
		{
			CheckTextBox(sender);
		}

		private void CheckTextBox(object sender)
		{
			if ((sender as TextBox).Name == "tbox_PlayerName")
				checkName = !string.IsNullOrWhiteSpace(((TextBox)sender).Text);

			if (enableCustomField)
			{
				if ((sender as TextBox).Name == "tbox_X")
					checkX = CheckNumber(sender);
				else
					checkY = CheckNumber(sender);
				btn_NewGame.IsEnabled = checkName && checkX & checkY;
			}
			else
			{
				btn_NewGame.IsEnabled = checkName;
			}
			
		}
		private bool CheckNumber(object sender)
		{
			bool sizeBool = int.TryParse(((TextBox)sender).Text, out int size);
			return !string.IsNullOrWhiteSpace(((TextBox)sender).Text) && sizeBool && size >= 3 && size <= 20;
		}
	}
}
