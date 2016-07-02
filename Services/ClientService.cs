using ShepScheduler.Areas.Statics;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Repository.Facade;

namespace ShepScheduler.Services
{
	public class ClientService
	{
		public static event EventHandler<Client> onSuccessSave;

		public static void OnSuccessSave(Client client)
		{
			if (onSuccessSave != null)
			{
				onSuccessSave(null, client);
			}
		}

		private static List<ClientBase> _clients;
		public static List<ClientBase> Clients
		{
			get
			{
				if (_clients == null)
				{
					_clients = new ClientsFacade().GetBaseClients();
				}
				return _clients;
			}
			set
			{
				_clients = value;
			}
		}

		public static Result Add(Client client)
		{
			Result result = new ClientsFacade().AddClient(client);
			if (result.NewIndex > 0)
			{
				client.Id = result.NewIndex;
				Clients.Add(client);
				Clients = Clients.OrderBy(m => m.Name).ToList();
				ClientPhotosFacade.CreateFolderForPhotos(client);
				OnSuccessSave(client);
			}
			return result;
		}

		public static Result Edit(Client client)
		{
			Result result = new ClientsFacade().EditClient(client);
			if (result.Success)
			{
				Clients.RemoveAll(m => m.Id == client.Id);
				Clients.Add(client);
				Clients = Clients.OrderBy(m => m.Name).ToList();
				OnSuccessSave(client);
			}
			return result;
		}

		public static Result Remove(ClientBase client)
		{
			Result result = new ClientsFacade().RemoveClient(client.Id);
			if (result.Success)
			{
				Clients.RemoveAll(m => m.Id == client.Id);
				ClientPhotosFacade.DeleteFolderForPhotos(client);
				OnSuccessSave(null);
			}
			return result;
		}

		public static Client GetClient(ClientBase clientBase)
		{
			Client client = new ClientsFacade().GetClient(clientBase.Id);
			return client;
		}
	}
}
