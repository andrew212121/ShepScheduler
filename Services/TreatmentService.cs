using ShepScheduler.Areas.Statics;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Repository.Facade;

namespace ShepScheduler.Services
{
	public class TreatmentService
	{
		private static List<Treatment> _treatments;
		public static List<Treatment> Treatments
		{
			get
			{
				if (_treatments == null)
				{
					_treatments = new TreatmentsFacade().GetTreatments();
				}
				return _treatments;
			}
			set
			{
				_treatments = value;
			}
		}

		public static Result Add(Treatment treatment)
		{
			var result = new TreatmentsFacade().AddTreatment(treatment);
			if (result.NewIndex > 0)
			{
				treatment.Id = result.NewIndex;
				Treatments.Add(treatment);
				Treatments = Treatments.OrderBy(m => m.Name).ToList();
				OnSuccessSave(treatment);
			}
			return result;
		}

		public static Result Edit(Treatment treatment)
		{
			var result = new TreatmentsFacade().EditTreatment(treatment);
			if (result.Success)
			{
				Treatments.RemoveAll(m => m.Id == treatment.Id);
				Treatments.Add(treatment);
				Treatments = Treatments.OrderBy(m => m.Name).ToList();
				OnSuccessSave(treatment);
			}
			return result;
		}

		public static Result Remove(Treatment treatment)
		{
			var result = new TreatmentsFacade().RemoveTreatment(treatment.Id);
			if (result.Success)
			{
				Treatments.RemoveAll(m => m.Id == treatment.Id);
				OnSuccessSave(treatment);
			}
			return result;
		}

		public static event EventHandler<Treatment> onSuccessSave;

		public static void OnSuccessSave(Treatment treatment)
		{
			if (onSuccessSave != null)
			{
				onSuccessSave(null, treatment);
			}
		}
	}
}
