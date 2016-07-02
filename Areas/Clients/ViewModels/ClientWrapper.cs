using ShepScheduler.Areas.Calendar.ViewModels;
using ShepScheduler.Core;
using ShepScheduler.Enums;
using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Clients.ViewModels
{
	public class ClientWrapper : ModelWrapper<Client>
	{
		public ClientWrapper(Client client)
			: base(client)
		{
			Photos = new ObservableCollection<PhotoWrapper>(client.Photos.Select(e => new PhotoWrapper(e)));
			RegisterCollection(Photos, client.Photos);
			Visits = new ObservableCollection<VisitWrapper>(client.Visits.Select(e => new VisitWrapper(e)));
			RegisterCollection(Visits, client.Visits);
		}

		public string FirstName
		{
			get { return GetValue<string>(); }
			set
			{
				SetValue(value);
			}
		}
		public string LastName
		{
			get { return GetValue<string>(); }
			set
			{
				SetValue(value);
			}
		}
		public string Phone
		{
			get { return GetValue<string>(); }
			set
			{
				SetValue(value);
			}
		}
		public string Comment
		{
			get { return GetValue<string>(); }
			set
			{
				SetValue(value);
			}
		}

		public ObservableCollection<PhotoWrapper> Photos
		{
			get; private set;
		}

		public ObservableCollection<VisitWrapper> Visits
		{
			get;
			private set;
		}
	}
}
