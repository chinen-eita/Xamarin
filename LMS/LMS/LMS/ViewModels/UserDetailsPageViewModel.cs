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
	public class UserDetailsPageViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _userId;

        public UserDetailsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            PageTitle = "About user";
            EditUserCommand = new Command(() =>
            {
                NavigationService.NavigateAsync(nameof(EditUserPage), new NavigationParameters { { "TEAM_ID", _userId } });
            });

            DeleteUserCommand = new Command(DeleteUserAsync);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }


        private void DeleteUserAsync()
        {
            LocalDataManager.WriteLocal(db => LocalDataManager.Delete<User>(db, _userId));
            NavigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _userId = parameters.GetValue<string>("TEAM_ID");
            var user = LocalDataManager.ReadLocal(db => db.Find<User>(_userId));

            LastName = user.FirstName;
            FirstName = user.LastName;
            
        }
    }
}
