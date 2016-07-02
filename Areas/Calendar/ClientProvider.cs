using ShepScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControls.Editors;

namespace ShepScheduler.Areas.Calendar
{
	public class ClientProvider : ISuggestionProvider
	{
		public System.Collections.IEnumerable GetSuggestions(string filter)
		{
			return ClientService.Clients.Where(fi => fi.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
		}

	}
}
