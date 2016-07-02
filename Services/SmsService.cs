using ShepScheduler.Helpers;
using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Services
{
	public class SmsService
	{
		public static void SendSmsForVisits(List<SmsVisit> visits, string template)
		{
			foreach(var visit in visits)
			{
				string message = SmsTemplateHelper.GetTextForVisitTSms(template, visit);

				visit.Message = SmsSenderHelper.SendSms(message, visit.Phone);
				if(string.IsNullOrEmpty(visit.Message))
				{
					visit.Message = "Wysłano";
				}
			}
		}
	}
}
