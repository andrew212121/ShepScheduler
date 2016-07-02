using ShepScheduler.Areas.Statics;
using ShepScheduler.Core;
using ShepScheduler.Models;
using ShepScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Treatments
{
	public class TreatmentValidator : ValidatorBase
	{
		private Treatment _model;

		public TreatmentValidator(Treatment treatment)
		{
			_model = treatment;
			ValidationRules = new List<Func<bool>> { ValidateName, ValidateDuration, ValidateDistinction };
		}

		private bool ValidateName()
		{
			if (string.IsNullOrEmpty(_model.Name))
			{
				ErrorMessage = "Nazwa jest wymagana!";
				return false;
			}
			return true;
		}

		private bool ValidateDuration()
		{
			if (_model.Duration <= 0)
			{
				ErrorMessage = "Czas trwania jest wymagany!";
				return false;
			}
			return true;
		}

		private bool ValidateDistinction()
		{
			if (TreatmentService.Treatments.Where(m => m.Id != _model.Id).Any(m => m.Name == _model.Name))
			{
				ErrorMessage = "Zabieg już istnieje!";
				return false;
			}
			return true;
		}
	}
}
