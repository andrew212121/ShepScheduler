using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Models
{
	public class Result
	{
		public Result()
		{
			Success = true;
		}

		public bool Success { get; set; }
		public string Message { get; set; }
		public int NewIndex { get; set; }
	}
}
