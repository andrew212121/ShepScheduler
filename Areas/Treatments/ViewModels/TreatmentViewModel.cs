using ShepScheduler.Areas.Statics;
using ShepScheduler.Core;
using ShepScheduler.Enums;
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
	public abstract class TreatmentViewModel : FormViewModelBase
	{
		protected Func<Treatment, Result> SaveAction { get; set; }
		public event EventHandler<Treatment> onSuccessSave;
		public void OnSuccessSave()
		{
			if (onSuccessSave != null)
			{
				onSuccessSave(this, ModelWrapper.Model);
			}
		}

		TreatmentWrapper _modelWrapper;
		public TreatmentWrapper ModelWrapper
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

		RelayCommand _saveTreatmentCommand;
		public ICommand SaveTreatmentCommand
		{
			get
			{
				if (_saveTreatmentCommand == null)
				{
					_saveTreatmentCommand = new RelayCommand(p => SaveTreatment(), p => true);
				}
				return _saveTreatmentCommand;
			}
		}

		private void SaveTreatment()
		{
			try
			{
				var validator = new TreatmentValidator(ModelWrapper.Model);
				if (validator.Validate())
				{
					var result = SaveAction(ModelWrapper.Model);
					if (result.Success)
					{
						OnSuccessSave();
					}
				}
				else
				{
					ErrorMessage = validator.ErrorMessage;
				}
			}
			catch (Exception ex)
			{
				ErrorMessage = "Wystąpił błąd. "+ex.Message;
			}

		}
	}
}
