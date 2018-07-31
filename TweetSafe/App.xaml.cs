using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Threading.Tasks;

using TweetSafe.Services;
using TweetSafe.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TweetSafe
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

			InitApp();
        }

        private void InitApp()
        {
            ViewModelLocator.RegisterDependencies();
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        private async Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();

            await navigationService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            base.OnStart();
                                     
            await InitNavigation();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
