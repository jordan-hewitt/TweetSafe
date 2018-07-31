using Xamarin.Forms;

using System.Threading.Tasks;
using System.Collections.Generic;
using TweetSafe.ViewModels;

namespace TweetSafe.Services
{
    public interface INavigationService
    {
        ViewModelBase PreviousPageViewModel { get; }

        IReadOnlyList<Page> GetNavigationStack { get; }

        void SetBarBackgroundColor(Color color);

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task NavigateToModalAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task PopModally();

        Task Pop();

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}
