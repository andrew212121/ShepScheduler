using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Calendar
{
	public class VisitSpaceDecorator
	{
		public static double GetMarginBottom(Visit current, Visit next)
		{
			var difference = next.StartDate - current.EndDate;
			var minutes = difference.TotalMinutes;

			if(minutes == 0)
			{
				return 0d;
			}
			if(minutes <= 15)
			{
				return 2d;
			}
			else if(minutes <= 60)
			{
				return 5d;
			}
			else if(minutes <= 179)
			{
				return 20d;
			}

			return 30d;
		}
	}
}
