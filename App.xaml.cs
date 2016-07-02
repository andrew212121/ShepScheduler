using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ShepScheduler.Views;
using ShepScheduler.Areas.Navigation.ViewModels;
using ShepScheduler.Helpers;
using ShepScheduler.Repository;
using ShepScheduler.Repository.Facade;

namespace ShepScheduler
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			bool hasLicense = LicenseHelper.CheckLicense();
			if (!hasLicense)
			{
				throw new Exception("Brak licencji!");
			}

			MainWindow app = new MainWindow();
			ApplicationViewModel context = new ApplicationViewModel();
			app.DataContext = context;
			app.Show();
		}

		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			//Handling the exception within the UnhandledExcpeiton handler.
			MessageBox.Show(e.Exception.Message + e.Exception.StackTrace, "Exception Caught", MessageBoxButton.OK, MessageBoxImage.Error);
			e.Handled = true;
		}
	}
}
