using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler
{
	public class Consts
	{
		public const string ClientPhotosPath = @"C:\ShepSchedulerData\ClientPhotos\";

		public static class SmsVisitTokens
		{
			public const string data = "{data}";
			public const string godzina = "{godzina}";
			public const string zabieg = "{zabieg}";
		}

		public static class AppConfigKeys
		{
			public const string PhoneNumberTest = "PhoneNumberTest";
			public const string SmsTemplateForVisit = "SmsTemplateForVisit";
			public const string TreatmentDuration = "TreatmentDuration";

			public const string SmsApiLogin = "SmsApiLogin";
			public const string SmsApiPassword = "SmsApiPassword";
			public const string SmsApiSender = "SmsApiSender";
		}
	}
}
