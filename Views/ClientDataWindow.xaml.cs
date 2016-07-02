using Microsoft.Win32;
using ShepScheduler.Areas.Clients.ViewModels;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Repository.Facade;
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
	/// Interaction logic for ClientDataWindow.xaml
	/// </summary>
	public partial class ClientDataWindow : Window
	{
		public ClientsDataViewModel ViewModel { get; set;}

		public ClientDataWindow(Client client, bool isEditable = true)
		{
			InitializeComponent();

			ViewModel = new ClientsDataViewModel(client, isEditable);
			this.DataContext = ViewModel;

			this.Show();
		}

		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() == true)
			{
				List<Photo> photos = ClientPhotosFacade.AddImagesForClient(ViewModel.ModelWrapper.Model, openFileDialog.FileNames);
				foreach(var photo in photos)
				{
					ViewModel.ModelWrapper.Photos.Add(new PhotoWrapper(photo));
				}
			}
		}
	}
}
