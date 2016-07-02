using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Calendar
{
	public class MonthInfo
	{
		public MonthInfo(DateTime StartDate)
		{
			this.StartDate = StartDate;
		}

		private List<Visit> _visits;
		public List<Visit> Visits
		{
			get{
				return _visits;
			}
			set
			{
				_visits = value;
			}
		}

		public DateTime StartDate { get; set; }
		private static CultureInfo _cultureInfo = new CultureInfo(CultureInfo.CurrentUICulture.LCID);
		private static System.Globalization.Calendar sysCal = _cultureInfo.Calendar;

		public string MonthName
		{
			get
			{
				return StartDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("pl"));
			}
		}

		public string Year
		{
			get
			{
				return StartDate.Year.ToString();
			}
		}

		public int DaysInMonth
		{
			get
			{
				return sysCal.GetDaysInMonth(StartDate.Year, StartDate.Month);
			}
		}

		public int StartOffsetDays
		{
			get
			{
				return GetDayOfWeek(StartDate);
			}
		}

		public int EndOffsetDays
		{
			get
			{
				return 7 - GetDayOfWeek(StartDate.AddDays(DaysInMonth - 1)) + 1;
			}
		}

		public int RowsNumber
		{
			get
			{
				return (DaysInMonth + StartOffsetDays + EndOffsetDays) / 7;
			}
		}

		private int GetDayOfWeek(DateTime date)
		{
			int day = (int)date.DayOfWeek;
			day = day > 0 ? --day : 6;
			return day;
		}
	}
}
