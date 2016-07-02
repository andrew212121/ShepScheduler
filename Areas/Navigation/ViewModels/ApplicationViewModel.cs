using ShepScheduler.Core;
using ShepScheduler.Helpers;
using ShepScheduler.Interfaces;
using ShepScheduler.Models;
using ShepScheduler.Repository;
using ShepScheduler.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShepScheduler.Areas.Navigation.ViewModels
{
	public class ApplicationViewModel : ViewModelBase
	{	
		private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

		public ApplicationViewModel()
        {
            // Add available pages
			PageViewModels.Add(new ClientsViewModel());
			PageViewModels.Add(new CalendarViewModel() { IsActive = true });
			PageViewModels.Add(new TreatmentsViewModel());

            // Set starting page
            CurrentPageViewModel = PageViewModels.First(m => m.IsActive);
        }

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    RaisePropertyChanged("CurrentPageViewModel");
                }
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

			CurrentPageViewModel.IsActive = false;

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);

			CurrentPageViewModel.IsActive = true;
        }
	}
}
