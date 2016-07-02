using ShepScheduler.Areas.Statics;
using ShepScheduler.Enums;
using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ShepScheduler.Areas.Calendar
{
	public class TreatmentColorDecorator
	{
		public static Brush DecorateWithColor(Visit visit)
		{
			if(visit.Treatment != null)
			{
				switch (visit.Treatment.Category)
				{
					case TreatmentCategory.Paznokcie:
						return Brushes.Coral;
					case TreatmentCategory.Skora:
						return Brushes.Yellow;
					case TreatmentCategory.Twarz:
						return Brushes.Plum;
				}
			}
			return Brushes.PaleGreen;
		}
	}
}
