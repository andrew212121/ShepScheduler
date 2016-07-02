using ShepScheduler.Areas.Statics;
using ShepScheduler.Core;
using ShepScheduler.Helpers;
using ShepScheduler.Interfaces;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Services;
using ShepScheduler.ShepEventArgs;
using ShepScheduler.UserControls;
using ShepScheduler.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShepScheduler.Areas.Navigation.ViewModels
{
	public class ClientsViewModel : ViewModelBase, IPageViewModel
	{
		public ClientsViewModel()
		{
			ClientService.onSuccessSave += RefreshClients;
		}

		#region Properties
			public string Name
			{
				get { return "Klienci"; }
			}

			private bool _isActive;
			public bool IsActive
			{
				get { return _isActive; }
				set
				{
					if (value != _isActive)
					{
						_isActive = value;
						RaisePropertyChanged("IsActive");
					}
				}
			}

			private ObservableCollection<ClientBase> _clients;
			public ObservableCollection<ClientBase> Clients
			{
				get
				{
					if(_clients == null)
					{
						RefreshClients();
					}
					return _clients;
				}
				set
				{
					_clients = value;
					RaisePropertyChanged("Clients");
				}
			}

			private string _nameFilter;
			public string NameFilter
			{
				get { return _nameFilter; }
				set
				{
					_nameFilter = value;
					RefreshClients();
				}
			}
		#endregion

		private void RefreshClients()
		{
			if (string.IsNullOrEmpty(NameFilter))
			{
				Clients = new ObservableCollection<ClientBase>(ClientService.Clients);
			}
			else
			{
				Clients = new ObservableCollection<ClientBase>(ClientService.Clients.Where(fi => fi.Name.IndexOf(NameFilter, StringComparison.OrdinalIgnoreCase) >= 0));
			}
		}

		private void NewClient()
		{
			var window = new CreateEditClientWindow(null);
		}

		private void RefreshClients(object sender, Client args)
		{
			RefreshClients();
		}

		public void ClientDetails(object p)
		{
			ClientBase client = (ClientBase)p;
			var clientFull = ClientService.GetClient(client);
			var window = new ClientDataWindow(clientFull);
		}

		public void RemoveClient(object p)
		{
			var client = (ClientBase)p;
			ClientService.Remove(client);
		}

		#region Commands
			RelayCommand _newClientCommand;
			public ICommand newClientCommand
			{
				get
				{
					if (_newClientCommand == null)
					{
						_newClientCommand = new RelayCommand(p => NewClient(), p => true);
					}
					return _newClientCommand;
				}
			}

			RelayCommand _detailsClientCommand;
			public RelayCommand detailsClientCommand
			{
				get
				{
					if (_detailsClientCommand == null)
					{
						_detailsClientCommand = new RelayCommand(p => ClientDetails(p), p => true);
					}
					return _detailsClientCommand;
				}
			}
			RelayCommand _removeClientCommand;
			public RelayCommand removeClientCommand
			{
				get
				{
					if (_removeClientCommand == null)
					{
						_removeClientCommand = new RelayCommand(p => RemoveClient(p), p => true);
					}
					return _removeClientCommand;
				}
			}
		#endregion
	}
}
