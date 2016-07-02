using ShepScheduler.Core;
using ShepScheduler.Enums;
using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Treatments.ViewModels
{
	public class TreatmentWrapper : ModelWrapper<Treatment>
	{
		public TreatmentWrapper(Treatment treatment)
			: base(treatment)
		{

		}

		public string Name
		{
			get { return GetValue<string>(); }
			set
			{
				SetValue(value);
			}
		}
		public int Duration
		{
			get { return GetValue<int>(); }
			set
			{
				SetValue(value);
			}
		}
		public decimal Price
		{
			get { return GetValue<decimal>(); }
			set
			{
				SetValue(value);
			}
		}
		public TreatmentCategory Category
		{
			get { return GetValue<TreatmentCategory>(); }
			set
			{
				SetValue(value);
			}
		}
	}
}
