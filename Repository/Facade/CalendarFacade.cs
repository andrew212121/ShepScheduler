using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Models;
using ShepScheduler.Repository.Core;
using ShepScheduler.Repository.Models;

namespace ShepScheduler.Repository.Facade
{
	public class CalendarFacade : BaseFacade
	{
		public List<Visit> GetVisitsForMonth(DateTime startMonth)
		{
			List<Visit> visits = new List<Visit>();
			using (var ctx = new ShepSchedulerDBContext())
			{
				DateTime nextMonth = startMonth.AddMonths(1);

				visits = ctx.Visits.Select(e => new Visit
				{
					Id = e.Id,
					Treatment = new Treatment
					{
						Name = e.Treatment.Name,
						Id = e.Treatment.Id,
						Duration = e.Treatment.Duration,
						Price = e.Treatment.Price,
						Category = e.Treatment.Category
					},
					Client = new ClientBase
					{
						Id = e.Client.Id,
						FirstName = e.Client.FirstName,
						LastName = e.Client.LastName,
						Phone = e.Client.Phone
					},
					Description = e.Description,
					Comment = e.Comment,
					StartDate = e.StartDate,
					EndDate = e.EndDate
				}).Where(m => m.StartDate >= startMonth && m.StartDate < nextMonth).ToList();

			}
			return visits;
		}

		public Result AddVisit(Visit visit)
		{
			Result result = Create<Visits>(visit);
			return result;
		}

		public Result EditVisit(Visit visit)
		{
			Result result = Edit<Visits>(visit);
			return result;
		}

		public Result RemoveVisit(int visitId)
		{
			Result result = Remove<Visits>(visitId);
			return result;
		}

		public Result RemoveVisits(List<int> visits)
		{
			Result result = Remove<Visits>(visits);
			return result;
		}

		public Result RemoveVisits(int treatmentId)
		{
			Result result = new Result();
			try
			{
				using (var ctx = new ShepSchedulerDBContext())
				{
					var visits = ctx.Visits.Where(m => m.TreatmentId == treatmentId);
					foreach (var visit in visits)
					{
						ctx.Visits.Remove(visit);
					}
					ctx.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = result.Message;
			}
			return result;
		}
	}
}
