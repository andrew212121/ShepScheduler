using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Statics
{
	public class ConfigurationData
	{
		private static ConfigurationData _instance;

		private ConfigurationData()
		{
		}

		public static ConfigurationData Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ConfigurationData();
				}
				return _instance;
			}
		}

		private string _phoneNumberTest;
		public string PhoneNumberTest
        {
			get
			{
				if (_phoneNumberTest == null)
				{
					_phoneNumberTest = ConfigurationManager.AppSettings[Consts.AppConfigKeys.PhoneNumberTest];
				}
				return _phoneNumberTest;				
			}
        }
		private string _smsTemplateForVisit;
		public string SmsTemplateForVisit
		{
			get
			{
				if (_smsTemplateForVisit == null)
				{
					_smsTemplateForVisit = ConfigurationManager.AppSettings[Consts.AppConfigKeys.SmsTemplateForVisit];
				}
				return _smsTemplateForVisit;
			}
		}
		private int? _treatmentDuration;
		public int TreatmentDuration
		{
			get
			{
				if (_treatmentDuration == null)
				{
					_treatmentDuration = Convert.ToInt32(ConfigurationManager.AppSettings[Consts.AppConfigKeys.TreatmentDuration]);
				}
				return (int)_treatmentDuration;
			}
		}
		private string _smsApiLogin;
		public string SmsApiLogin
		{
			get
			{
				if (_smsApiLogin == null)
				{
					_smsApiLogin = ConfigurationManager.AppSettings[Consts.AppConfigKeys.SmsApiLogin];
				}
				return _smsApiLogin;
			}
		}
		private string _smsApiPassword;
		public string SmsApiPassword
		{
			get
			{
				if (_smsApiPassword == null)
				{
					_smsApiPassword = ConfigurationManager.AppSettings[Consts.AppConfigKeys.SmsApiPassword];
				}
				return _smsApiPassword;
			}
		}
		private string _smsApiSender;
		public string SmsApiSender
		{
			get
			{
				if (_smsApiSender == null)
				{
					_smsApiSender = ConfigurationManager.AppSettings[Consts.AppConfigKeys.SmsApiSender];
				}
				return _smsApiSender;
			}
		}

		private bool? _isTestEnvironment;
		public bool IsTestEnvironment
		{
			get
			{
				if (_isTestEnvironment == null)
				{
					_isTestEnvironment = ConfigurationManager.AppSettings[Consts.AppConfigKeys.Environment] == "DEV";
				}
				return _isTestEnvironment.Value;
			}
		}
	}
}
