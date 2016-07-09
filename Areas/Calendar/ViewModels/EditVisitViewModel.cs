using ShepScheduler.Helpers;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.ShepEventArgs;
using ShepScheduler.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ShepScheduler.Repository.Facade;
using ShepScheduler.Core;

namespace ShepScheduler.Areas.Calendar.ViewModels
{
	public class EditVisitViewModel : VisitViewModel
	{
		public EditVisitViewModel(VisitWrapper visitWrapper, MonthInfo monthInfo) : base(monthInfo)
		{
			this.ModelWrapper = visitWrapper;
			ModelWrapper.Phone = ModelWrapper.Model.Client.Phone;
			ModelWrapper.VisitDate = visitWrapper.Model.StartDate;
			ModelWrapper.IsEditMode = true;
			WindowTitle = "Edytuj wizytę";
		}

		protected override void SaveVisit()
		{
			try
			{
				var validator = new VisitValidator(ModelWrapper.Model, MonthInfo);

				ModelWrapper.StartDate = new DateTime(ModelWrapper.VisitDate.Year, ModelWrapper.VisitDate.Month, ModelWrapper.VisitDate.Day, ModelWrapper.StartDate.Hour, ModelWrapper.StartDate.Minute, 0);
				ModelWrapper.EndDate = new DateTime(ModelWrapper.VisitDate.Year, ModelWrapper.VisitDate.Month, ModelWrapper.VisitDate.Day, ModelWrapper.EndDate.Hour, ModelWrapper.EndDate.Minute, 0);

				if (validator.Validate())
				{
					Result result = CalendarService.EditVisit(ModelWrapper.Model);
					if (result.Success)
					{
						OnSuccessSave(this, EventArgs.Empty);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorMessage = "Wystąpił błąd. " + ex.Message;
			}
		}
		private void DeleteVisit()
		{
			try
			{
				Result result = CalendarService.DeleteVisit(ModelWrapper.Model);
				if (result.Success)
				{
					if(MonthInfo != null)
					{ 
						MonthInfo.Visits.Remove(ModelWrapper.Model);
					}
					if (onSuccessDelete != null) onSuccessDelete(this, null);
				}
			}
			catch (Exception ex)
			{
				ErrorMessage = "Wystąpił błąd. " + ex.Message;
			}
		}

		public event EventHandler<Visit> onSuccessDelete;

		RelayCommand _deleteVisitCommand;
		public ICommand deleteVisitCommand
		{
			get
			{
				if (_deleteVisitCommand == null)
				{
					_deleteVisitCommand = new RelayCommand(p => DeleteVisit(), p => true);
				}
				return _deleteVisitCommand;
			}
		}
	}
}
