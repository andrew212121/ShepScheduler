using ShepScheduler.Helpers;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Services;
using ShepScheduler.ShepEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShepScheduler.Areas.Clients.ViewModels
{
	public class CreateClientViewModel : ClientViewModel
	{
		public CreateClientViewModel()
		{
			ModelWrapper = new ClientWrapper(new Client());
			WindowTitle = "Dodaj klienta";
			SaveAction = ClientService.Add;
		}
	}
}
