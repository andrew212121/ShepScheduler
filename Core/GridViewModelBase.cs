using ShepScheduler.Areas.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShepScheduler.Core
{
	public class GridViewModelBase : ViewModelBase
	{
		public Visibility Visibility
		{
			get
			{
				if (ConfigurationData.Instance.IsTestEnvironment)
				{
					return Visibility.Visible;
				}
				return Visibility.Collapsed;
			}
		}
	}
}
