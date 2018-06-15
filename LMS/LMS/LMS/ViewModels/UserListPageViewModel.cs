using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LMS.Library.Data;
using LMS.Library.Utility;
using LMS.Models;
using LMS.Views;
using Prism.Navigation;
using Xamarin.Forms;
using Realms;

namespace LMS.ViewModels
{
	public class UserListPageViewModel : ViewModelBase
    {
        private ObservableCollection<User> _allUsers;

        public UserListPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            PageTitle = "All Users";

            AddUserCommand = new Command(() => { NavigationService.NavigateAsync(nameof(AddUserPage)); });

            SelectedUserCommand = new Command<string>(item =>
            {
                NavigationService.NavigateAsync(nameof(UserDetailsPage), new NavigationParameters
                {
                    {"TEAM_ID", item}
                });
            });

            AllUsers = LocalDataManager.ReadLocal(realm => realm.All<User>()).AsObserveble();

        }

        public ObservableCollection<User> AllUsers
        {
            get => _allUsers;
            set => SetProperty(ref _allUsers, value);
        }

        public ICommand AddUserCommand { get; }
        public ICommand SelectedUserCommand { get; }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            AllUsers = LocalDataManager.ReadLocal(realm => realm.All<User>()).AsObserveble();
        }
    }
}
