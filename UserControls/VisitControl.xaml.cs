using ShepScheduler.Areas.Calendar;
using ShepScheduler.Areas.Calendar.ViewModels;
using ShepScheduler.Models;
using ShepScheduler.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShepScheduler.UserControls
{
	/// <summary>
	/// Interaction logic for VisitControl.xaml
	/// </summary>
	public partial class VisitControl : UserControl
	{
		private MonthInfo _monthInfo;
		public event EventHandler<Visit> onVisitChange;

		public VisitControl(MonthInfo monthInfo)
		{
			_monthInfo = monthInfo; 
			InitializeComponent();
		}

		private void DayBoxVisit_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var visit = _monthInfo.Visits.First(m => m.Id == (int)Tag);
			var window = new CreateEditVisitWindow(new VisitWrapper(visit), _monthInfo);
			window.ViewModel.onSuccessSave += onVisitChange;
			((EditVisitViewModel)window.ViewModel).onSuccessDelete += onVisitChange;
			e.Handled = true;
		}
	}
}
