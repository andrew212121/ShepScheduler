using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Areas.Clients.ViewModels;
using ShepScheduler.Models;
using ShepScheduler.Repository.Core;
using ShepScheduler.Repository.Models;

namespace ShepScheduler.Repository.Facade
{
	public class ClientsFacade : BaseFacade
	{
		public List<ClientBase> GetBaseClients()
		{
			List<ClientBase> clients = new List<ClientBase>();
			using (var ctx = new ShepSchedulerDBContext())
			{

				clients = ctx.Clients.Select(e => new ClientBase
				{
					Id = e.Id,
					FirstName = e.FirstName,
					LastName = e.LastName,
					Phone = e.Phone
				}).OrderBy(m => m.LastName).ToList();

			}
			return clients;
		}

		public Client GetClient(int clientId)
		{
			Client client;
			using (var ctx = new ShepSchedulerDBContext())
			{

				client = ctx.Clients.Select(e => new Client
				{
					Id = e.Id,
					FirstName = e.FirstName,
					LastName = e.LastName,
					Phone = e.Phone,
					Visits = e.Visits.Select(m => new Visit
					{
						Id = m.Id,
						StartDate = m.StartDate,
						EndDate = m.EndDate,
						Comment = m.Comment,
						Description = m.Description,
						Treatment = new Treatment
						{
							Name = m.Treatment.Name
						},
					}).ToList()
				}).First(m => m.Id == clientId);

			}
			return client;
		}

		public Result AddClient(Client client)
		{
			Result result = Create<Clients>(client);
			return result;
		}

		public Result EditClient(Client client)
		{
			Result result = Edit<Clients>(client);
			return result;
		}

		public Result RemoveClient(int clientId)
		{
			var client = GetClient(clientId);
			Result result = new CalendarFacade().RemoveVisits(client.Visits.Select(m => m.Id).ToList());
			if(result.Success)
			{ 
				result = Remove<Clients>(clientId);
			}
			return result;
		}
	}
}
