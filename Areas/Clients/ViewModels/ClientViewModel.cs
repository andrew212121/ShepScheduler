using ShepScheduler.Areas.Treatments.ViewModels;
using ShepScheduler.Core;
using ShepScheduler.Helpers;
using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShepScheduler.Areas.Clients.ViewModels
{
	public abstract class ClientViewModel : FormViewModelBase
	{
		public event EventHandler<Client> onSuccessSave;
		protected Func<Client, Result> SaveAction { get; set; }

		protected void OnSuccessSave(Client client)
		{
			if(onSuccessSave != null) onSuccessSave(this, client);
		}

		ClientWrapper _modelWrapper;
		public ClientWrapper ModelWrapper
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

		RelayCommand _saveClientCommand;
		public ICommand saveClientCommand
		{
			get
			{
				if (_saveClientCommand == null)
				{
					_saveClientCommand = new RelayCommand(p => SaveClient(), p => true);
				}
				return _saveClientCommand;
			}
		}

		private void SaveClient()
		{
			var validator = new ClientValidator(ModelWrapper.Model);
			if (validator.Validate())
			{
				var result = SaveAction(ModelWrapper.Model);
				if (result.Success)
				{
					OnSuccessSave(ModelWrapper.Model);
				}
			}
			else
			{
				ErrorMessage = validator.ErrorMessage;
			}
		}
	}
}
