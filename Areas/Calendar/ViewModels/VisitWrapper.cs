using ShepScheduler.Core;
using ShepScheduler.Enums;
using ShepScheduler.Models;
using ShepScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Calendar.ViewModels
{
	public class VisitWrapper : ModelWrapper<Visit>
	{
		public VisitWrapper(Visit visit)
			: base(visit)
		{
			ClientName = visit.ClientName;
			TreatmentName = visit.TreatmentName;
		}

		public string Comment
		{
			get { return GetValue<string>(); }
			set
			{
				SetValue(value);
			}
		}
		public string Description
		{
			get { return GetValue<string>(); }
			set
			{
				SetValue(value);
			}
		}
		public DateTime StartDate
		{
			get { return GetValue<DateTime>(); }
			set
			{
				SetValue(value);
			}
		}
		public DateTime EndDate
		{
			get { return GetValue<DateTime>(); }
			set
			{
				SetValue(value);
			}
		}
		public DateTime _visitDate;
		public DateTime VisitDate
		{
			get { return _visitDate; }
			set
			{
				_visitDate = value;
				RaisePropertyChanged("VisitDate");
			}
		}
		public bool _isEditMode;
		public bool IsEditMode
		{
			get { return _isEditMode; }
			set
			{
				_isEditMode = value;
				RaisePropertyChanged("IsEditMode");
			}
		}
		private string _phone;
		public string Phone
		{
			get { return _phone; }
			set
			{
				_phone = value;
				RaisePropertyChanged("Phone");
			}
		}
		public Treatment Treatment
		{
			get
			{
				if (Model.Treatment == null && !string.IsNullOrEmpty(TreatmentName))
				{
					Model.Treatment = TreatmentService.Treatments.FirstOrDefault(m => m.Name == TreatmentName);
				}
				return Model.Treatment;
			}
			set
			{
				Model.Treatment = value;
				RaisePropertyChanged("Treatment");
			}
		}
		public ClientBase Client
		{
			get
			{
				if (Model.Client == null && !string.IsNullOrEmpty(ClientName))
				{
					Model.Client = ClientService.Clients.FirstOrDefault(m => m.Name == ClientName);
				}
				return Model.Client;
			}
			set
			{
				Model.Client = value;
				RaisePropertyChanged("Client");
			}
		}
		private string _clientName;
		public string ClientName
		{
			get
			{
				return _clientName;
			}
			set
			{
				_clientName = value;
				RaisePropertyChanged("ClientName");
			}
		}
		private string _treatmentName;
		public string TreatmentName
		{
			get { return _treatmentName; }
			set
			{
				_treatmentName = value;
				RaisePropertyChanged("TreatmentName");
			}
		}

		//public void SetTreatment()
		//{
		//	Model.Treatment = TreatmentService.Treatments.FirstOrDefault(m => m.Name == TreatmentName);
		//}
		//public void SetClient()
		//{
		//	Model.Client = ClientService.Clients.FirstOrDefault(m => m.Name == ClientName);
		//}
	}
}
