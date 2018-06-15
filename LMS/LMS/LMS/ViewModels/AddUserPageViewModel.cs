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
	public class AddUserPageViewModel : ViewModelBase
    {
        public AddUserPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            PageTitle = "Add new user";
            SaveUserCommand = new Command(SaveUser);
        }

        private string _lastName;
        private string _firstName;
        
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
            LocalDataManager.WriteLocal(db => LocalDataManager.Insert(db,
                new User { LastName = LastName, FirstName = FirstName}));
            NavigationService.GoBackAsync();
        }
    }
}
