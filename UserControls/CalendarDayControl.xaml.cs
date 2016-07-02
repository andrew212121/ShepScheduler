using ShepScheduler.Areas.Calendar;
using ShepScheduler.Areas.Calendar.ViewModels;
using ShepScheduler.Areas.Statics;
using ShepScheduler.Enums;
using ShepScheduler.Models;
using ShepScheduler.Services;
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
	/// Interaction logic for CalendarDayControl.xaml
	/// </summary>
	public partial class CalendarDayControl : UserControl
	{
		private List<Visit> _visits;
		private DateTime _dayDate;
		private MonthInfo _monthInfo;
		private CalendarControl _calendarControl;

		public CalendarDayControl(int dayInMonth, MonthInfo monthInfo, CalendarControl calendarControl)
		{
			_dayDate = new DateTime(monthInfo.StartDate.Year, monthInfo.StartDate.Month, dayInMonth);
			_monthInfo = monthInfo;
			_calendarControl = calendarControl;

			InitializeComponent();

			DayNumberLabel.Content = dayInMonth;
			Tag = _dayDate;
			btnNewAppointment.Tag = _dayDate;
			RefreshVisitsForDay();
			RedrawVisits();
		}

		private void btnNewAppointment_Click(object sender, RoutedEventArgs e)
		{
			var window = new CreateEditVisitWindow(_dayDate, _monthInfo);
			window.ViewModel.onSuccessSave += OnVisitsChanged;
		}

		private void btnShowTotalPrice_Click(object sender, RoutedEventArgs e)
		{
			var treatments = _visits.Where(m => m.Treatment != null).Select(m => m.Treatment);
			decimal total = 0M;
			foreach(var treatment in treatments)
			{
				var fullTreatment = TreatmentService.Treatments.FirstOrDefault(m => m.Name == treatment.Name);
				if(fullTreatment != null)
				{
					total += fullTreatment.Price;
				}
			}

			MessageBox.Show(string.Format("Suma cen zabiegów dnia {0} to {1}", _dayDate.ToString("dd-MM-yyyy"), total.ToString("C")));
		}

		private void OnVisitsChanged(object sender, Visit visit)
		{
			if(visit != null && visit.StartDate.Day != _dayDate.Day)
			{
				var changedDayControl = _calendarControl.DaysControls.First(m => m._dayDate.Day == visit.StartDate.Day);
				changedDayControl.RefreshVisitsForDay();
				changedDayControl.RedrawVisits();
			}
 
			RefreshVisitsForDay();
			RedrawVisits();
		}

		private void RefreshVisitsForDay()
		{
			_visits = _monthInfo.Visits.FindAll(m => m.StartDate.Day == _dayDate.Day);
		}
		private void RedrawVisits()
		{
			DayAppointmentsStack.Children.Clear();
			var visits = _visits.OrderBy(m => m.StartDate).ToList();

			for(int i=0; i<visits.Count; i++)
			{
				VisitControl visitBox = new VisitControl(_monthInfo);
				visitBox.DisplayText.Text = visits[i].MonthCellTitle;
				visitBox.Tag = visits[i].Id;
				visitBox.onVisitChange += OnVisitsChanged;
				DayAppointmentsStack.Children.Add(visitBox);

				visitBox.BorderElement.Background = TreatmentColorDecorator.DecorateWithColor(visits[i]);
				visitBox.Margin = new Thickness(0, 0, 0, 10);

				if(i < visits.Count-1)
				{ 
					double marginBottom = VisitSpaceDecorator.GetMarginBottom(visits[i], visits[i+1]);
					visitBox.Margin = new Thickness(0, 0, 0, marginBottom);
				}
			}
		}

		private void btnSendSms_Click(object sender, RoutedEventArgs e)
		{
			var window = new DaySmsWindow(_visits, _dayDate);
		}
	}
}
