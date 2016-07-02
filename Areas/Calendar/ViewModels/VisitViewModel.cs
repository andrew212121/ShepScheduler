using ShepScheduler.Areas.Statics;
using ShepScheduler.Core;
using ShepScheduler.Helpers;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Repository.Facade;
using ShepScheduler.Services;
using ShepScheduler.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShepScheduler.Areas.Calendar.ViewModels
{
	public abstract class VisitViewModel : FormViewModelBase
	{
		protected MonthInfo MonthInfo;

		public VisitViewModel(MonthInfo monthInfo)
		{
			MonthInfo = monthInfo;
		}

		public VisitViewModel() { }

		VisitWrapper _modelWrapper;
		public VisitWrapper ModelWrapper
		{
			get
			{
				return _modelWrapper;
			}
			set
			{
				_modelWrapper = value;
				RaisePropertyChanged("ModelWrapper");
			}
		}

		public List<Treatment> Treatments
		{
			get
			{
				return TreatmentService.Treatments;
			}
		}

		public List<ClientBase> Clients
		{
			get
			{
				return ClientService.Clients;
			}
		}

		RelayCommand _saveVisitCommand;
		public ICommand saveVisitCommand
		{
			get
			{
				if (_saveVisitCommand == null)
				{
					_saveVisitCommand = new RelayCommand(p => SaveVisit(), p => true);
				}
				return _saveVisitCommand;
			}
		}

		public event EventHandler<Visit> onSuccessSave;

		protected abstract void SaveVisit();

		public void SetTimeForTreatment()
		{
			if (ModelWrapper.Treatment != null && ModelWrapper.Treatment.Duration > 0)
			{
				ModelWrapper.EndDate = ModelWrapper.StartDate.AddMinutes(ModelWrapper.Treatment.Duration);
			}
		}

		protected void OnSuccessSave(object sender, EventArgs args)
		{
			if (onSuccessSave != null) onSuccessSave(sender, ModelWrapper.Model);
		}

		RelayCommand _showHistoryCommand;
		public ICommand showHistoryCommand
		{
			get
			{
				if (_showHistoryCommand == null)
				{
					_showHistoryCommand = new RelayCommand(p => ShowClientHistory(), p => true);
				}
				return _showHistoryCommand;
			}
		}

		private void ShowClientHistory()
		{
			if(ModelWrapper.Client != null)
			{ 
				var client = new ClientsFacade().GetClient(ModelWrapper.Client.Id);
				var window = new ClientDataWindow(client, false);
			}
		}
	}
}
