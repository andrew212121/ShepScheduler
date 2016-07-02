using ShepScheduler.Areas.Navigation.ViewModels;
using ShepScheduler.Models;
using ShepScheduler.Repository;
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

namespace ShepScheduler.UserControls
{
	/// <summary>
	/// Interaction logic for ClientsControl.xaml
	/// </summary>
	public partial class ClientsControl : UserControl
	{
		public ClientsControl()
		{
			InitializeComponent();				
		}

		public void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			((ClientsViewModel)DataContext).ClientDetails(ClientsDataGrid.SelectedItem);
			e.Handled = true;			
		}
	}
}
