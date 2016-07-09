using ShepScheduler.Areas.Clients;
using ShepScheduler.Areas.Statics;
using ShepScheduler.Areas.Treatments;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Services;
using ShepScheduler.ShepEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Calendar.ViewModels
{
	public class CreateVisitViewModel : VisitViewModel
	{
		public CreateVisitViewModel(DateTime date, MonthInfo monthInfo)
			: base(monthInfo)
		{
			ModelWrapper = new VisitWrapper(new Visit());
			ModelWrapper.StartDate = new DateTime(date.Year, date.Month, date.Day, DateTime.Now.Hour, 0, 0);
			ModelWrapper.EndDate = this.ModelWrapper.StartDate.AddHours(2);
			ModelWrapper.VisitDate = ModelWrapper.StartDate;
			ModelWrapper.IsEditMode = false;
			WindowTitle = "Dodaj wizytę";
		}

		protected override void SaveVisit()
		{
			try
			{ 
				bool valid = true;
				if (ModelWrapper.Client == null && ModelWrapper.ClientName != "") 
				{
					valid = AddNewClient();
				} 
				if (ModelWrapper.Treatment == null && ModelWrapper.TreatmentName != "")
				{
					valid = AddNewTreatment();
				}

				if(valid)
				{
					var validator = new VisitValidator(ModelWrapper.Model, MonthInfo);
					if(validator.Validate())
					{
						Result result = CalendarService.AddVisit(ModelWrapper.Model);
						if (result.NewIndex > 0)
						{
							ModelWrapper.Model.Id = result.NewIndex;
							MonthInfo.Visits.Add(ModelWrapper.Model);
							if (result.Success) OnSuccessSave(this, EventArgs.Empty);
						}
					}
					else
					{
						ErrorMessage = validator.ErrorMessage;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorMessage = "Wystąpił błąd. " + ex.Message;
			}
		}

		private bool AddNewTreatment()
		{
			var treatment = new Treatment{
				Name = ModelWrapper.TreatmentName,
				Duration = 60
			};

			var treatmentValidator = new TreatmentValidator(treatment);
			if(treatmentValidator.Validate())
			{
				Result result = TreatmentService.Add(treatment);
				if(result.Success)
				{
					treatment.Id = result.NewIndex;
					ModelWrapper.Treatment = treatment;
					return true;
				}
			}
			else
			{
				ErrorMessage = treatmentValidator.ErrorMessage;
			}
			return false;
		}

		private bool AddNewClient()
		{
			if (ModelWrapper.ClientName.Split(' ').Count() == 2)
			{
				var names = ModelWrapper.ClientName.Split(' ');

				var client = new Client{
					FirstName = names[1],
					LastName = names[0],
					Phone = ModelWrapper.Phone
				};

				var clientValidator = new ClientValidator(client);
				if(clientValidator.Validate())
				{
					ClientService.Add(client);
					ModelWrapper.Client = client;
					return true;
				}
				else
				{
					ErrorMessage = clientValidator.ErrorMessage;
				}
			}
			else
			{
				ErrorMessage = "Klient ma format: \"Nazwisko Imię\"";
			}
			return false;
		}
	}
}
