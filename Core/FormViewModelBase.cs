using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Core
{
	public class FormViewModelBase : ViewModelBase
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set
			{
				_errorMessage = value;
				RaisePropertyChanged();
			}
		}

		private string _windowTitle;
		public string WindowTitle
		{
			get { return _windowTitle; }
			set
			{
				_windowTitle = value;
				RaisePropertyChanged();
			}
		}
	}
}
