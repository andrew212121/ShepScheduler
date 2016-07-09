using ShepScheduler.Areas.Statics;
using ShepScheduler.Core;
using ShepScheduler.Helpers;
using ShepScheduler.Interfaces;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Services;
using ShepScheduler.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShepScheduler.Areas.Navigation.ViewModels
{
	public class TreatmentsViewModel : GridViewModelBase, IPageViewModel
	{
		public TreatmentsViewModel()
		{
			TreatmentService.onSuccessSave += RefreshTreatments;
		}

		public string Name
		{
			get { return "Zabiegi"; }
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
		private string _nameFilter;
		public string NameFilter
		{
			get { return _nameFilter; }
			set
			{
				_nameFilter = value;
				RefreshTreatments();
			}
		}

		private ObservableCollection<Treatment> _treatments;
		public ObservableCollection<Treatment> Treatments
		{
			get
			{
				if (_treatments == null)
				{
					RefreshTreatments();
				}
				return _treatments;
			}
			set
			{
				_treatments = value;
				RaisePropertyChanged("Treatments");
			}
		}

		private void NewTreatment()
		{
			var window = new CreateEditTreatmentWindow(null);
		}

		private void RefreshTreatments(object sender, Treatment e)
		{
			RefreshTreatments();
		}

		private void RefreshTreatments()
		{
			if (string.IsNullOrEmpty(NameFilter))
			{
				Treatments = new ObservableCollection<Treatment>(TreatmentService.Treatments);
			}
			else
			{
				Treatments = new ObservableCollection<Treatment>(TreatmentService.Treatments.Where(fi => fi.Name.IndexOf(NameFilter, StringComparison.OrdinalIgnoreCase) >= 0));
			}
		}

		public void RemoveTreatment(object p)
		{
			TreatmentService.Remove((Treatment)p);
		}

		private void EditTreatment(object p)
		{
			var window = new CreateEditTreatmentWindow((Treatment)p);
		}

		#region Commands
			RelayCommand _newTreatmentCommand;
			public ICommand NewTreatmentCommand
			{
				get
				{
					if (_newTreatmentCommand == null)
					{
						_newTreatmentCommand = new RelayCommand(p => NewTreatment(), p => true);
					}
					return _newTreatmentCommand;
				}
			}
			RelayCommand _removeTreatmentCommand;
			public RelayCommand RemoveTreatmentCommand
			{
				get
				{
					if (_removeTreatmentCommand == null)
					{
						_removeTreatmentCommand = new RelayCommand(p => RemoveTreatment(p), p => true);
					}
					return _removeTreatmentCommand;
				}
			}
			RelayCommand _editTreatmentCommand;
			public RelayCommand EditTreatmentCommand
			{
				get
				{
					if (_editTreatmentCommand == null)
					{
						_editTreatmentCommand = new RelayCommand(p => EditTreatment(p), p => true);
					}
					return _editTreatmentCommand;
				}
			}
		#endregion
	}
}
