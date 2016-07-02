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
	public class TreatmentsFacade : BaseFacade
	{
		public List<Treatment> GetTreatments()
		{
			List<Treatment> treatments = new List<Treatment>();
			using (var ctx = new ShepSchedulerDBContext())
			{
				treatments = ctx.Treatments.Select(e => new Treatment
				{
					Id = e.Id,
					Name = e.Name,
					Duration = e.Duration,
					Price = e.Price,
					Category = e.Category
				}).OrderBy(m => m.Name).ToList();

			}
			return treatments;
		}

		public Result AddTreatment(Treatment treatment)
		{
			var result = Create<Treatments>(treatment);
			return result;
		}

		public Result EditTreatment(Treatment treatment)
		{
			Result result = Edit<Treatments>(treatment);
			return result;
		}

		public Result RemoveTreatment(int treatmentId)
		{
			Result result = new CalendarFacade().RemoveVisits(treatmentId);
			if(result.Success)
			{ 
				result = Remove<Treatments>(treatmentId);
			}
			return result;
		}
	}
}
