using ShepScheduler.Areas.Calendar;
using ShepScheduler.Areas.Navigation.ViewModels;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.ShepEventArgs;
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
	/// Interaction logic for CalendarControl.xaml
	/// </summary>
	public partial class CalendarControl : UserControl
	{
		public List<CalendarDayControl> DaysControls = new List<CalendarDayControl>();

		public CalendarControl()
		{
			InitializeComponent();
			Loaded += CalendarControl_Loaded;
			MonthInfo = new MonthInfo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
			MonthBuilder = new CalendarBuilder(MonthInfo, this);
		}

		public async void GetVisitsForMonth()
		{
			var _viewModel = (CalendarViewModel)DataContext;
			_viewModel.PanelLoading = true;
			List<Visit> visits = await GetVisitsAsync();
			SetVisitsForMonth(visits);
			_viewModel.PanelLoading = false;
		}

		private async Task<List<Visit>> GetVisitsAsync()
		{
			return await Task.Run(() =>
			{
				var visits = CalendarService.GetVisitsForMonth(MonthInfo.StartDate);
				return visits;
			});
		}

		private void CalendarControl_Loaded(object sender, RoutedEventArgs e)
		{
			GetVisitsForMonth();			
		}

		private void DisplayMonthChanged(CalendarMonthChangedEventArgs e)
		{
			GetVisitsForMonth();
		}

		public MonthInfo MonthInfo;
		private CalendarBuilder MonthBuilder;

		public void SetVisitsForMonth(List<Visit> visits)
		{
			MonthInfo.Visits = visits;
			MonthBuilder.BuildCalendarUI();
		}

		private void UpdateMonth(int MonthsToAdd)
		{
			MonthInfo.StartDate = MonthInfo.StartDate.AddMonths(MonthsToAdd);
			DisplayMonthChanged(null);
		}

		#region UI Event Handlers
			private void MonthGoPrev_MouseLeftButtonUp(System.Object sender, MouseButtonEventArgs e)
			{
				UpdateMonth(-1);
			}

			private void MonthGoNext_MouseLeftButtonUp(System.Object sender, MouseButtonEventArgs e)
			{
				UpdateMonth(1);
			}
		#endregion
	}
}
