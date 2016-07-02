using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ShepScheduler.Areas.Statics;

namespace ShepScheduler.Helpers
{
	public class SmsSenderHelper
	{
		public static string SendSms(string text, string phone)
		{
			string errorMessage = CheckIfInternetAvailable();
			if(errorMessage != "") return errorMessage;
			errorMessage = CheckIfPhoneCorrect(phone);
			if(errorMessage != "") return errorMessage;

			try
			{
				SMSApi.Api.Client client = new SMSApi.Api.Client(ConfigurationData.Instance.SmsApiLogin);
				client.SetPasswordHash(ConfigurationData.Instance.SmsApiPassword);

				var smsApi = new SMSApi.Api.SMSFactory(client);

				var result =
					smsApi.ActionSend()
						.SetText(text)
						.SetTo(phone)
						.SetSender(ConfigurationData.Instance.SmsApiSender) //Pole nadawcy lub typ wiadomość 'ECO', '2Way'
						.Execute();
			}
			catch (SMSApi.Api.ActionException e)
			{
				/**
				 * Błędy związane z akcją (z wyłączeniem błędów 101,102,103,105,110,1000,1001 i 8,666,999,201)
				 * http://www.smsapi.pl/sms-api/kody-bledow
				 */
				errorMessage = e.Message;
			}
			catch (SMSApi.Api.ClientException e)
			{
				/**
				 * 101 Niepoprawne lub brak danych autoryzacji.
				 * 102 Nieprawidłowy login lub hasło
				 * 103 Brak punków dla tego użytkownika
				 * 105 Błędny adres IP
				 * 110 Usługa nie jest dostępna na danym koncie
				 * 1000 Akcja dostępna tylko dla użytkownika głównego
				 * 1001 Nieprawidłowa akcja
				 */
				errorMessage = e.Message;
			}
			catch (SMSApi.Api.HostException e)
			{
				/* błąd po stronie servera lub problem z parsowaniem danych
				 * 
				 * 8 - Błąd w odwołaniu
				 * 666 - Wewnętrzny błąd systemu
				 * 999 - Wewnętrzny błąd systemu
				 * 201 - Wewnętrzny błąd systemu
				 * SMSApi.Api.HostException.E_JSON_DECODE - problem z parsowaniem danych
				 */
				errorMessage = e.Message;
			}
			catch (SMSApi.Api.ProxyException e)
			{
				// błąd w komunikacji pomiedzy klientem a serverem
				errorMessage = e.Message;
			}
			return errorMessage;
		}

		private static string CheckIfInternetAvailable()
		{
			try
			{
				using (var client = new WebClient())
				{
					using (var stream = client.OpenRead("http://www.google.com"))
					{
						return "";
					}
				}
			}
			catch
			{
				return "Brak połączenia z internetem.";
			}
		}

		public static string CheckIfPhoneCorrect(string phone)
		{
			string message = "";
			Regex reg = new Regex(@"^\d{9}$");
			if(phone == null || !reg.IsMatch(phone))
			{
				message = "Nieprawidłowy nr telefonu.";
			}

			return message;
		}
	}
}
