using Prism;
using Prism.Ioc;
using LMS.ViewModels;
using LMS.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LMS.Library.Data;
using Prism.Mvvm;
using Prism.DryIoc;
using System.Reflection;
using System.Globalization;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LMS
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            //Ipsalyerアプリケーションの初期化
            IpsalyzerApp.Initialize();

            //await NavigationService.NavigateAsync("NavigationPage/UserListPage");
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            //Ipsalyerアプリケーションの初期化
            IpsalyzerApp.Initialize();

            //await NavigationService.NavigateAsync("NavigationPage/UserListPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<UserListPage>();
            containerRegistry.RegisterForNavigation<AddUserPage>();
            containerRegistry.RegisterForNavigation<UserDetailsPage>();
            containerRegistry.RegisterForNavigation<EditUserPage>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            //ViewModelLocationProvider.Register<UserListPage>(() => new UserListPage());
            //ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(
            //    viewType =>
            //    {
            //        var viewName = viewType.FullName;
            //        viewName = viewName.Replace(".Views.", ".ViewModels.");
            //        string viewModelName;
            //        if (viewName.EndsWith("Page"))
            //        {
            //            viewModelName = $"{viewName.Substring(0, viewName.LastIndexOf("Page", StringComparison.Ordinal))}ViewModel";
            //        }
            //        else
            //        {
            //            var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
            //            viewModelName = $"{viewName}{suffix}";
            //        }
            //        var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            //        return Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewModelName, viewAssemblyName));
            //    });
        }
    }
}
