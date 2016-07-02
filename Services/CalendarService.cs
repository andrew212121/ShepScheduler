using ShepScheduler.Areas.Statics;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Repository.Facade;
using ShepScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Calendar
{
	public class CalendarService
	{
		public static Result AddVisit(Visit visit)
		{
			var result = new CalendarFacade().AddVisit(visit);
			return result;
		}
		public static Result EditVisit(Visit visit)
		{
			var result = new CalendarFacade().EditVisit(visit);
			return result;
		}
		public static Result DeleteVisit(Visit visit)
		{
			var result = new CalendarFacade().RemoveVisit(visit.Id);
			return result;
		}

		public static List<Visit> GetVisitsForMonth(DateTime startDate)
		{
			var facade = new CalendarFacade();
			var visits = facade.GetVisitsForMonth(startDate);

			return visits;
		}
	}
}
