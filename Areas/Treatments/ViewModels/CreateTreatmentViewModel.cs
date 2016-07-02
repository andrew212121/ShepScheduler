using ShepScheduler.Areas.Statics;
using ShepScheduler.Helpers;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShepScheduler.Areas.Treatments.ViewModels
{
	public class CreateTreatmentViewModel : TreatmentViewModel
	{
		public CreateTreatmentViewModel()
		{
			var model = new Treatment{
				Duration=ConfigurationData.Instance.TreatmentDuration
			};
			ModelWrapper = new TreatmentWrapper(model);
			WindowTitle = "Nowy zabieg";
			SaveAction = TreatmentService.Add;
		}
	}
}
