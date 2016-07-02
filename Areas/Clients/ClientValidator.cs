using ShepScheduler.Areas.Statics;
using ShepScheduler.Core;
using ShepScheduler.Helpers;
using ShepScheduler.Models;
using ShepScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Clients
{
	public class ClientValidator : ValidatorBase
	{
		private Client _client;

		public ClientValidator(Client client)
		{
			_client = client;
			ValidationRules = new List<Func<bool>> { ValidateLastName, ValidateFirstName, ValidateDistinction, ValidatePhone };
		}

		private bool ValidatePhone()
		{
			if (!string.IsNullOrEmpty(_client.Phone))
			{
				ErrorMessage = SmsSenderHelper.CheckIfPhoneCorrect(_client.Phone);
				if(ErrorMessage != "") return false;
			}
			return true;
		}

		private bool ValidateLastName()
		{
			if (string.IsNullOrEmpty(_client.LastName))
			{
				ErrorMessage = "Nazwisko jest wymagane!";
				return false;
			}
			return true;
		}

		private bool ValidateFirstName()
		{
			if (string.IsNullOrEmpty(_client.FirstName))
			{
				ErrorMessage = "Imię jest wymagane!";
				return false;
			}
			return true;
		}

		private bool ValidateDistinction()
		{
			if (ClientService.Clients.Where(m => m.Id != _client.Id).Any(m => m.FirstName == _client.FirstName &&
				m.LastName == _client.LastName && m.Phone == _client.Phone))
			{
				ErrorMessage = "Klient już istnieje!";
				return false;
			}
			return true;
		}
	}
}
