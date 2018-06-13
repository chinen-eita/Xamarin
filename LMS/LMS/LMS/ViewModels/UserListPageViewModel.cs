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


namespace LMS.ViewModels
{
	public class UserListPageViewModel : ViewModelBase
    {
        private ObservableCollection<User> _allTeams;

        public UserListPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            PageTitle = "All Teams";

            //AddTeamCommand = new Command(() => { NavigationService.NavigateAsync(nameof(AddTeamPage)); });

            //SelectedTeamCommand = new Command<string>(item =>
            //{
            //    NavigationService.NavigateAsync(nameof(TeamDetailsPage), new NavigationParameters
            //    {
            //        {"TEAM_ID", item}
            //    });
            //});
        }
	}
}
