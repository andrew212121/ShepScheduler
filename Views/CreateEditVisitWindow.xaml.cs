using ShepScheduler.Areas.Calendar;
using ShepScheduler.Areas.Calendar.ViewModels;
using ShepScheduler.Models;
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
using System.Windows.Shapes;

namespace ShepScheduler.Views
{
	/// <summary>
	/// Interaction logic for EditVisitWindow.xaml
	/// </summary>
	public partial class CreateEditVisitWindow : Window
	{
		internal VisitViewModel ViewModel;
		private bool _contentRendered;

		public CreateEditVisitWindow(VisitWrapper visit, MonthInfo monthInfo)
		{
			InitializeComponent();
			this.ViewModel = new EditVisitViewModel(visit, monthInfo);
			this.DataContext = ViewModel;
			this.ViewModel.onSuccessSave += (s, e) => this.Close();
			((EditVisitViewModel)this.ViewModel).onSuccessDelete += (s, e) => this.Close();

			ContentRendered += EditVisitWindow_Loaded;
			this.Show();


		}

		public CreateEditVisitWindow(DateTime date, MonthInfo monthInfo)
		{
			InitializeComponent();
			this.ViewModel = new CreateVisitViewModel(date, monthInfo);
			this.DataContext = ViewModel;
			this.ViewModel.onSuccessSave += (s, e) => this.Close();
			ContentRendered += EditVisitWindow_Loaded;
			this.Show();
		}

		private void EditVisitWindow_Loaded(object sender, EventArgs e)
		{
			_contentRendered = true;
		}

		private void OnStartDateChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (_contentRendered) ViewModel.SetTimeForTreatment();
		}

		private void Treatment_SelectedItemChanged(object sender, WpfControls.Editors.ItemEventArgs e)
		{
			ViewModel.SetTimeForTreatment();
		}

		private void Client_SelectedItemChanged(object sender, WpfControls.Editors.ItemEventArgs e)
		{
			ViewModel.ModelWrapper.Phone = ViewModel.ModelWrapper.Client.Phone;
		}
	}
}
