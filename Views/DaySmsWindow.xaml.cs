using ShepScheduler.Areas.Calendar.ViewModels;
using ShepScheduler.Models;
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

namespace ShepScheduler.Views
{
	/// <summary>
	/// Interaction logic for DaySmsWindow.xaml
	/// </summary>
	public partial class DaySmsWindow : Window
	{
		public DaySmsViewModel ViewModel { get; private set;}

		public DaySmsWindow(List<Visit> visits, DateTime dayDate)
		{
			InitializeComponent();

			ViewModel = new DaySmsViewModel(visits, dayDate);
			this.DataContext = ViewModel;
			this.ViewModel.onSuccessSave += (s, e) => this.Close();

			this.Show();
		}
	}
}
