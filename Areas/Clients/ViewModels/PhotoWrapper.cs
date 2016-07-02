using ShepScheduler.Core;
using ShepScheduler.Enums;
using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Clients.ViewModels
{
	public class PhotoWrapper : ModelWrapper<Photo>
	{
		public PhotoWrapper(Photo photo)
			: base(photo)
		{

		}

		public string Name
		{
			get { return GetValue<string>(); }
			set
			{
				SetValue(value);
			}
		}
		public string Path
		{
			get { return GetValue<string>(); }
			set
			{
				SetValue(value);
			}
		}
	}
}
