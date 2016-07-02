using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Models
{
	public class SmsVisit
	{
		public string Client { get; set;}
		public string Phone { get; set;}
		public string Treatment { get; set;}
		public string Message { get; set;}
		public bool SendingEnabled { get; set; }
		public DateTime Date { get; set;}
	}
}
