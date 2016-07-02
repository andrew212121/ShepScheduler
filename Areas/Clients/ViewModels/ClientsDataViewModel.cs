using ShepScheduler.Areas.Calendar.ViewModels;
using ShepScheduler.Areas.Statics;
using ShepScheduler.Core;
using ShepScheduler.Helpers;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Repository.Facade;
using ShepScheduler.Services;
using ShepScheduler.ShepEventArgs;
using ShepScheduler.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShepScheduler.Areas.Clients.ViewModels
{
	public class ClientsDataViewModel : ViewModelBase
	{
		public ClientsDataViewModel(Client client, bool isEditable = true)
		{
			SetClientPhotos(client);
			ModelWrapper = new ClientWrapper(client);
			IsEditable = isEditable;

			if(isEditable)
			{
				Visibility = Visibility.Visible;
			}
			else
			{
				Visibility = Visibility.Collapsed;
			}
		}

		#region properties
			ClientWrapper _modelWrapper;
			public ClientWrapper ModelWrapper
			{
				get
				{
					return _modelWrapper;
				}
				set
				{
					_modelWrapper = value;
					RaisePropertyChanged("ModelWrapper");
				}
			}

			private bool _isEditable;
			public bool IsEditable
			{
				get { return _isEditable; }
				set
				{
					_isEditable = value;
					RaisePropertyChanged("IsEditable");
				}
			}
			private Visibility _visibility;
			public Visibility Visibility
			{
				get
				{
					return _visibility;
				}
				set
				{
					_visibility = value;
					RaisePropertyChanged("Visibility");
				}
			}
		#endregion

		RelayCommand _editClientCommand;
		public ICommand editClientCommand
		{
			get
			{
				if (_editClientCommand == null)
				{
					_editClientCommand = new RelayCommand(p => EditClient(), p => true);
				}
				return _editClientCommand;
			}
		}

		RelayCommand _openEditWindowCommand;
		public RelayCommand openEditWindowCommand
		{
			get
			{
				if (_openEditWindowCommand == null)
				{
					_openEditWindowCommand = new RelayCommand(p => EditVisit(p), p => true);
				}
				return _openEditWindowCommand;
			}
			set
			{
				_openEditWindowCommand = value;
			}
		}

		private void EditVisit(object p)
		{
			var visit = p as VisitWrapper;
			visit.Client = ModelWrapper.Model;
			visit.ClientName = visit.Client.Name;
			visit.Treatment = TreatmentService.Treatments.FirstOrDefault(m => m.Name == visit.TreatmentName);
			var window = new CreateEditVisitWindow(visit, null);
		}

		private void EditClient()
		{
			var window = new CreateEditClientWindow(ModelWrapper);
		}

		public void SetClientPhotos(Client client)
		{
			var paths = ClientPhotosFacade.GetImagesForClient(client);
			foreach (var path in paths)
			{
				var photo = new Photo
				{
					Path = path,
					Name = Path.GetFileNameWithoutExtension(path)
				};
				client.Photos.Add(photo);
			}
		}

		RelayCommand _deletePhotoCommand;
		public RelayCommand deletePhotoCommand
		{
			get
			{
				if (_deletePhotoCommand == null)
				{
					_deletePhotoCommand = new RelayCommand(p => DeletePhoto(p), p => true);
				}
				return _deletePhotoCommand;
			}
			set
			{
				_deletePhotoCommand = value;
			}
		}

		private void DeletePhoto(object p)
		{
			PhotoWrapper photo = (PhotoWrapper)p;

			if (File.Exists(photo.Path))
			{
				File.Delete(photo.Path);
			}
			ModelWrapper.Photos.Remove(photo);
		}
	}
}
