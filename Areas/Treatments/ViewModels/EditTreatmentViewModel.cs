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
	public class EditTreatmentViewModel : TreatmentViewModel
	{
		public EditTreatmentViewModel(Treatment treatment)
		{
			ModelWrapper = new TreatmentWrapper(treatment);
			WindowTitle = "Edytuj zabieg";
			SaveAction = TreatmentService.Edit;
		}
	}
}
