using ShepScheduler.Areas.Clients.ViewModels;
using ShepScheduler.Models;
using ShepScheduler.ShepEventArgs;
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
	/// Interaction logic for EditClientWindow.xaml
	/// </summary>
	public partial class CreateEditClientWindow : Window
	{
		public ClientViewModel ViewModel;

		public CreateEditClientWindow(ClientWrapper client)
		{
			InitializeComponent();

			if (client != null)
			{
				this.ViewModel = new EditClientViewModel(client);
			}
			else
			{
				ViewModel = new CreateClientViewModel();
			}
			this.DataContext = ViewModel;
			this.ViewModel.onSuccessSave += (s, e) => this.Close();
			this.Show();
		}
	}
}
