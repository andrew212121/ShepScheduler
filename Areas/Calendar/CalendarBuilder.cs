using ShepScheduler.Models;
using ShepScheduler.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShepScheduler.Areas.Calendar
{
	public class CalendarBuilder
	{
		private MonthInfo _monthInfo;
		private CalendarControl _calendarControl;

		public CalendarBuilder(MonthInfo monthInfo, CalendarControl calendarControl)
		{
			_monthInfo = monthInfo;
			_calendarControl = calendarControl;
		}

		internal void BuildCalendarUI()
		{
			int DaysInMonth = _monthInfo.DaysInMonth;
			int StartOffsetDays = _monthInfo.StartOffsetDays;
			int iWeekCount = 0;
			CalendarRowControl weekRowCtrl = new CalendarRowControl();

			_calendarControl.MonthViewGrid.Children.Clear();
			AddRowDefinitionsToGrid(DaysInMonth, StartOffsetDays);
			SetCalendarLabel();

			for (int i = 1; i <= DaysInMonth; i++)
			{
				if (CheckIfWeekIsFinished(i, StartOffsetDays))
				{
					//-- add existing weekrowcontrol to the monthgrid
					Grid.SetRow(weekRowCtrl, iWeekCount);
					_calendarControl.MonthViewGrid.Children.Add(weekRowCtrl);
					//-- make a new weekrowcontrol
					weekRowCtrl = new CalendarRowControl();
					iWeekCount += 1;
				}

				CalendarDayControl dayBox = new CalendarDayControl(i, _monthInfo, _calendarControl);
				_calendarControl.DaysControls.Add(dayBox);

				Grid.SetColumn(dayBox, (i - (iWeekCount * 7)) + StartOffsetDays);
				weekRowCtrl.WeekRowGrid.Children.Add(dayBox);
			}
			Grid.SetRow(weekRowCtrl, iWeekCount);
			_calendarControl.MonthViewGrid.Children.Add(weekRowCtrl);
		}

		//-- customize daybox for today:
		private void StyleCellIfToday(int i, CalendarDayControl dayBox)
		{
			if ((new System.DateTime(_monthInfo.StartDate.Year, _monthInfo.StartDate.Month, i)) == System.DateTime.Today)
			{
				dayBox.DayLabelRowBorder.Background = (Brush)dayBox.TryFindResource("OrangeGradientBrush");
				dayBox.DayAppointmentsStack.Background = Brushes.Wheat;
			}
		}

		private void SetCalendarLabel()
		{
			_calendarControl.MonthYearLabel.Content = _monthInfo.MonthName + " " + _monthInfo.Year;
		}

		private void AddRowDefinitionsToGrid(int DaysInMonth, int OffSetDays)
		{
			_calendarControl.MonthViewGrid.RowDefinitions.Clear();
			GridLength rowHeight = new GridLength(60, GridUnitType.Star);

			int rowsNumber = _monthInfo.RowsNumber;
			for (int i = 1; i <= rowsNumber; i++)
			{
				var rowDef = new RowDefinition();
				rowDef.Height = rowHeight;
				_calendarControl.MonthViewGrid.RowDefinitions.Add(rowDef);
			}
		}

		private static bool CheckIfWeekIsFinished(int i, int iOffsetDays)
		{
			var dayNumber = i + iOffsetDays - 1;
			if (i != 1 && (dayNumber % 7) == 0)
			{
				return true;
			}
			return false;
		}
	}
}
