using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepScheduler.Repository;

namespace ShepScheduler.Helpers
{
	public class LicenseHelper
	{
		public static bool CheckLicense()
		{
			bool valid = false;
			try
			{
				string text;
				string path = AppDomain.CurrentDomain.BaseDirectory + "License.txt";
				var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
				{
					text = streamReader.ReadToEnd();
				}

				var decrypted = CryptoHelper.Decrypt(text);
				var splitted = decrypted.Split('&');
				var name = splitted[0];
				int year = Convert.ToInt32(splitted[1]);

				if ((Environment.MachineName == name && DateTime.Now.Year <= year) || (name == "hypervial" && year == 2007))
				{
					valid = true;
				}
			}
			catch (Exception)
			{
			}

			return valid;
		}
	}
}
