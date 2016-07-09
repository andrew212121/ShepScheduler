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
	public class EditClientViewModel : ClientViewModel
	{
		public EditClientViewModel(ClientWrapper clientWrapper)
		{
			ModelWrapper = clientWrapper; 
			WindowTitle = "Edytuj klienta";
			SaveAction = ClientService.Edit;
		}
	}
}
