using ShepScheduler.Areas.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Interfaces;
using ShepScheduler.Repository.Core;
using ShepScheduler.Repository.Models;

namespace ShepScheduler.Models
{
	public class Visit : IEditModel<Visits>, ICreateModel<Visits>
	{
		public int Id { get; set; }
		public Treatment Treatment { get; set;}
		public ClientBase Client { get; set; }
		public string Description { get; set; }
		public string Comment { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public string TreatmentName
		{
			get
			{
				if (Treatment != null) return Treatment.Name;
				return "";
			}
		}
		public string ClientName
		{
			get
			{
				if (Client != null) return Client.Name;
				return "";
			}
		}
		public string MonthCellTitle
		{
			get
			{
				string startTime = StartDate.ToShortTimeString();
				string endTime = EndDate.ToShortTimeString();
				return startTime + "-" + endTime + " " + Client.Name + " - " + TreatmentName;
			}
		}


		public void FillEntity(Visits entity)
		{
			entity.StartDate = StartDate;
			entity.EndDate = EndDate;
			entity.ClientId = Client.Id;
			entity.TreatmentId = Treatment.Id;
			entity.Description = Description;
			entity.Comment = Comment;
		}

		public void FillNewEntity(Visits entity)
		{
			FillEntity(entity);
		}
	}
}
