using ShepScheduler.Helpers;
using ShepScheduler.Models;
using ShepScheduler.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ShepScheduler.Areas.Statics;
using ShepScheduler.Core;

namespace ShepScheduler.Areas.Calendar.ViewModels
{
	public class DaySmsViewModel : ViewModelBase
	{
		public DaySmsViewModel(List<Visit> visits, DateTime dayDate)
		{
			_visits = new ObservableCollection<SmsVisit>(visits.Select(m => new SmsVisit{
				Client = m.Client.Name,
				Phone = m.Client.Phone,
				Treatment = m.TreatmentName,
				Date = m.StartDate,
				SendingEnabled = true
			}).ToList());

			_visits.Add(new SmsVisit
			{
				Client = "TestClient",
				Phone = ConfigurationData.Instance.PhoneNumberTest,
				Treatment = "TestTreatment",
				Date = new DateTime(dayDate.Year, dayDate.Month, dayDate.Day, 1, 0, 0),
				SendingEnabled = true
			});

			_template = ConfigurationData.Instance.SmsTemplateForVisit;
			_tokens = string.Format("Tokeny: {0}, {1}, {2}", Consts.SmsVisitTokens.data, Consts.SmsVisitTokens.godzina, Consts.SmsVisitTokens.zabieg);
		}

		public event EventHandler<EventArgs> onSuccessSave;

		private string _template;
		public string Template
		{
			get { return _template; }
			set
			{
				_template = value;
				RaisePropertyChanged("Template");
			}
		}
		private string _tokens;
		public string Tokens
		{
			get { return _tokens; }
			set
			{
				_tokens = value;
				RaisePropertyChanged("Tokens");
			}
		}

		private ObservableCollection<SmsVisit> _visits;
		public ObservableCollection<SmsVisit> Visits
		{
			get
			{
				return _visits;
			}
			set
			{
				_visits = value;
				RaisePropertyChanged("Visits");
			}
		}

		#region Commands
			RelayCommand _sendSmsCommand;
			public ICommand SendSmsCommand
			{
				get
				{
					if (_sendSmsCommand == null)
					{
						_sendSmsCommand = new RelayCommand(p => SendSms(), p => true);
					}
					return _sendSmsCommand;
				}
			}

			private void SendSms()
			{
				var visitsToSend = _visits.Where(m => m.SendingEnabled).ToList();
				SmsService.SendSmsForVisits(visitsToSend, Template);
				Visits = new ObservableCollection<SmsVisit>(_visits);
			}
		#endregion
	}
}
