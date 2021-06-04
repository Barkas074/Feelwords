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

namespace Feelwords.WPF
{
	/// <summary>
	/// Interaction logic for Rating.xaml
	/// </summary>
	public partial class Rating : Window
	{
		public Rating()
		{
			InitializeComponent();
		}

		private void btn_Back_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new();
			mainWindow.Show();
			Close();
		}
	}
}
