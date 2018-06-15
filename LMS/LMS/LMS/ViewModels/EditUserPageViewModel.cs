using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LMS.Library.Data;
using LMS.Models;
using Prism.Navigation;
using Xamarin.Forms;
using Realms;

namespace LMS.ViewModels
{
	public class EditUserPageViewModel : ViewModelBase
    {
        private string _lastName;
        private string _firstName;
        private string _userId;

        public EditUserPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            PageTitle = "Edit user";
            SaveUserCommand = new Command(SaveUser);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public ICommand SaveUserCommand { get; }

        private void SaveUser()
        {
            LocalDataManager.WriteLocal(db =>
            {
                var user = db.Find<User>(_userId);
                user.LastName = LastName;
                user.FirstName = FirstName;
                LocalDataManager.Update(db, user);
            });

            NavigationService.GoBackAsync(new NavigationParameters { { "TEAM_ID", _userId } });
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _userId = parameters.GetValue<string>("TEAM_ID");
            var user = LocalDataManager.ReadLocal(db => db.Find<User>(_userId));

            LastName = user.LastName;
            FirstName = user.FirstName;
        }
    }
}
