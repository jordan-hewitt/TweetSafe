using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using TweetSafe.Helpers;

namespace TweetSafe.Droid
{
    [Activity(Label = "TweetSafe", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Constants.ScreenWidth = Resources.DisplayMetrics.WidthPixels / (double)Resources.DisplayMetrics.Density;
            Constants.ScreenHeight = Resources.DisplayMetrics.HeightPixels / (double)Resources.DisplayMetrics.Density;
            Constants.ScaleFactor = Resources.DisplayMetrics.Density;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

