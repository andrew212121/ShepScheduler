using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Helpers
{
	public class SmsTemplateHelper
	{
		public static string GetTextForVisitTSms(string template, SmsVisit visit)
		{
			string message = "";

			message = template.Replace(Consts.SmsVisitTokens.data, visit.Date.ToString("dd-MM-yyyy"));
			message = message.Replace(Consts.SmsVisitTokens.godzina, visit.Date.ToString("HH:mm"));
			message = message.Replace(Consts.SmsVisitTokens.zabieg, visit.Treatment);

			return message;
		}
	}
}
