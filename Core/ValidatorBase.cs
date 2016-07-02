using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Core
{
	public class ValidatorBase
	{
		public string ErrorMessage { get; protected set; }
		protected List<Func<bool>> ValidationRules { get; set; }


		public bool Validate()
		{
			bool valid = true;
			foreach (var rule in ValidationRules)
			{
				if (!rule())
				{
					valid = false;
					break;
				}
			}

			return valid;
		}
	}
}
