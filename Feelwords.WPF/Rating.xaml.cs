using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
	/// Interaction logic for Rating.xaml
	/// </summary>
	public partial class Rating : Window
	{
		public Rating(List<RatingInfo> ratingInfo)
		{
			InitializeComponent();
			RatingInfoDataGrid ratingInfoDataGrid = (RatingInfoDataGrid)Resources["ratingInfo"];
			for (int i = 0; i < ratingInfo.Count; i++)
			{
				ratingInfoDataGrid.Add(ratingInfo[i]);
			}
		}
		private void btn_Back_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new();
			mainWindow.Show();
			Close();
		}
	}

	public class RatingInfoDataGrid : ObservableCollection<RatingInfo>
	{
		// Создание коллекции рейтинга таким образом позволяет привязывать данные из XAML.
	}
}
