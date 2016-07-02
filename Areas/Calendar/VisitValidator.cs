using ShepScheduler.Core;
using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Calendar
{
	public class VisitValidator : ValidatorBase
	{
		private Visit _visit;
		private MonthInfo _monthInfo;

		public VisitValidator(Visit visit, MonthInfo monthInfo)
		{
			_visit = visit;
			_monthInfo = monthInfo;
			ValidationRules = new List<Func<bool>>{ ValidateClient, ValidateTreatment, ValidateTime, ValidateOtherVisitsTime };
		}

		private bool ValidateTime()
		{
			if(_visit.EndDate <= _visit.StartDate)
			{
				ErrorMessage = "Godzina zakończenia nie może być wcześniej niż rozpoczecia!";
				return false;
			}
			return true;
		}

		private bool ValidateTreatment()
		{
			if (_visit.Treatment == null)
			{
				ErrorMessage = "Pole zabieg jest obowiązkowe!";
				return false;
			}
			return true;
		}

		private bool ValidateClient()
		{
			if (_visit.Client == null)
			{
				ErrorMessage = "Pole klient jest obowiązkowe!";
				return false;
			}
			return true;
		}

		private bool ValidateOtherVisitsTime()
		{
			IEnumerable<Visit> visitsInDay;
			if(_monthInfo != null)
			{ 
				visitsInDay = _monthInfo.Visits.Where(m => m.StartDate.Day == _visit.StartDate.Day);
			}
			else
			{
				visitsInDay = CalendarService.GetVisitsForMonth(_visit.StartDate).Where(m => m.StartDate.Day == _visit.StartDate.Day);
			}
			foreach(var visit in visitsInDay)
			{
				if(visit.Id != _visit.Id)
				{
					if(visit.StartDate < _visit.EndDate && _visit.StartDate < visit.EndDate)
					{
						ErrorMessage = "Już istnieje wizyta w tych godzinach!";
						return false;
					}
				}
			}
			return true;
		}
	}
}
