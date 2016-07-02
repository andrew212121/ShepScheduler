using ShepScheduler.Core;
using ShepScheduler.Interfaces;
using ShepScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepScheduler.Areas.Navigation.ViewModels
{
	public class CalendarViewModel : ViewModelBase, IPageViewModel
	{
		public string Name
		{
			get { return "Kalendarz"; }
		}

		private bool _isActive;
		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				if (value != _isActive)
				{
					_isActive = value;
					RaisePropertyChanged("IsActive");
				}
			}
		}

		private bool _panelLoading;
		public bool PanelLoading
		{
			get
			{
				return _panelLoading;
			}
			set
			{
				_panelLoading = value;
				RaisePropertyChanged("PanelLoading");
			}
		}
	}
}
