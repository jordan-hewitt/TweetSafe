using Xamarin.Forms;
using System;
using System.Reflection;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using TweetSafe.Views;
using TweetSafe.ViewModels;

namespace TweetSafe.Services
{
    public class NavigationService : INavigationService
    {
        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as NavigationPage;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as ViewModelBase;
            }
        }

        public IReadOnlyList<Page> GetNavigationStack
        {
            get
            {
                var mainPage = Application.Current.MainPage as NavigationPage;
                return mainPage.Navigation.NavigationStack;
            }
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<MainViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null, false);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter, false);
        }

        public Task NavigateToModalAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null, true);
        }

        public Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter, true);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        public void SetBarBackgroundColor(Color color)
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
                mainPage.BarBackgroundColor = color;
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool isModal)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (page is MainView)
            {
                //Application.Current.MainPage = new NavigationPage(page);
                Application.Current.MainPage = new NavigationPage(page);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as NavigationPage;

                if (navigationPage != null && isModal)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(page);
                }
                else if (page != null && !isModal)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).Initialize(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page;

            if (pageType == typeof(MainView))
                page = Activator.CreateInstance(pageType) as ContentPage;
            else
                page = Activator.CreateInstance(pageType) as BaseView;

            return page;
        }

        public async Task PopModally()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async Task Pop()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
