using LMS.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LMS.Library.Data;
using LMS.Library.Utility;
using LMS.Models;

namespace LMS.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            PageTitle = "Top Page";

            NavToListCommand = new Command<string>(item =>
            {
                NavigationService.NavigateAsync(nameof(UserListPage));
            });
        }

        public ICommand NavToListCommand { get; }
    }
}
