using Xamarin.Forms;
using System.Collections.ObjectModel;
using TweetSafe.ViewModels;

/** This class is a custom renderer that permits the programmer to use icons on the left side of the 
 * toolbar for iOS. Any page that would normally use a ContentPage to inherit off of can use this instead
 * to do so.  Once the page is left, the icons from the left side of the toolbar are removed.                                                                                
 */

namespace TweetSafe.Views
{
    public class BaseView : ContentPage
    {
        public ObservableCollection<ToolbarItem> LeftToolbarItems { get; set; }

        public BaseView()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            NavigationPage.SetHasNavigationBar(this, false);

            LeftToolbarItems = new ObservableCollection<ToolbarItem>();

            SetBinding(TitleProperty, new Binding(ViewModelBase.TitlePropertyName));
            SetBinding(IconProperty, new Binding(ViewModelBase.IconPropertyName));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (LeftToolbarItems.Count > 0)
                LeftToolbarItems.Clear();
        }

        public static BindableProperty BarButtonColorProperty =
            BindableProperty.CreateAttached("BarButtonColor", typeof(Color), typeof(BaseView), Color.Blue,
                BindingMode.OneWay);

        public static Color GetBarButtonColor(BindableObject view)
        {
            return (Color)view.GetValue(BarButtonColorProperty);
        }

        public static void SetBarButtonColor(BindableObject view, Color value)
        {
            view.SetValue(BarButtonColorProperty, value);
        }

        public static BindableProperty BarColorProperty =
            BindableProperty.CreateAttached("BarColor", typeof(Color), typeof(BaseView), Color.White,
                BindingMode.OneWay);

        public static Color GetBarColor(BindableObject view)
        {
            return (Color)view.GetValue(BarColorProperty);
        }

        public static void SetBarColor(BindableObject view, Color value)
        {
            view.SetValue(BarColorProperty, value);
        }

        public static BindableProperty BarTitleColorProperty =
            BindableProperty.CreateAttached("BarTitleColor", typeof(Color), typeof(BaseView), Color.White,
                BindingMode.OneWay);

        public static Color GetBarTitleColor(BindableObject view)
        {
            return (Color)view.GetValue(BarTitleColorProperty);
        }

        public static void SetBarTitleColor(BindableObject view, Color value)
        {
            view.SetValue(BarTitleColorProperty, value);
        }
    }
}

