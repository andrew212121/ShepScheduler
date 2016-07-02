using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Helpers
{
	public static class FileReaderHelper
	{
		public static string GetTextFromFile(string path)
		{
			string text = null;
			if (File.Exists(path))
			{
				using (var streamReader = new StreamReader(path, Encoding.UTF8))
				{
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}
	}
}
