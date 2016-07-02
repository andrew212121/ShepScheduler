using ShepScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControls.Editors;

namespace ShepScheduler.Areas.Calendar
{
	public class TreatmentProvider : ISuggestionProvider
	{
		public System.Collections.IEnumerable GetSuggestions(string filter)
		{
			return TreatmentService.Treatments.Where(fi => fi.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
		}

	}
}
