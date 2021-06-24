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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Feelwords.Logic;

namespace Feelwords.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private GameInfo gameInfo;
		private readonly List<RatingInfo> ratingInfo;
		public MainWindow()
		{
			InitializeComponent();
			new WorkWithFiles().ReadFiles(out ratingInfo, out gameInfo);
			if (gameInfo == null || ratingInfo.Count == 0)
			{
				btn_Continue.IsEnabled = false;
			}
			else
			{
				btn_Continue.IsEnabled = true;
			}
		}

		private void btn_Rating_Click(object sender, RoutedEventArgs e)
		{
			Rating rating = new(ratingInfo);
			rating.Show();
			Close();
		}

		private void btn_UserDefinition_Click(object sender, RoutedEventArgs e)
		{
			UserDefinition userDefinition = new(ratingInfo);
			userDefinition.Show();
			Close();
		}

		private void btn_Exit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void btn_Continue_Click(object sender, RoutedEventArgs e)
		{
			Game game = new(gameInfo, ratingInfo);
			game.Show();
			Close();
		}

		private void btn_Settings_Click(object sender, RoutedEventArgs e)
		{
			Settings settings = new();
			settings.Show();
			Close();
		}
	}
}
