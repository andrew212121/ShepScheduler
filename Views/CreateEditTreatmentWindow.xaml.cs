﻿using ShepScheduler.Areas.Treatments.ViewModels;
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
	/// Interaction logic for CreateTreatmentWindow.xaml
	/// </summary>
	public partial class CreateEditTreatmentWindow : Window
	{
		public TreatmentViewModel ViewModel { get; private set; }

		public CreateEditTreatmentWindow(Treatment treatment)
		{
			InitializeComponent();

			if (treatment == null)
			{
				ViewModel = new CreateTreatmentViewModel();
			}
			else
			{
				ViewModel = new EditTreatmentViewModel(treatment);
			}
			this.DataContext = ViewModel;
			this.ViewModel.onSuccessSave += (s, e) => this.Close();

			this.Show();
		}
	}
}
