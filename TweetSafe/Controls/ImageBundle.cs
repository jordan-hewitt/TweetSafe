using Xamarin.Forms;

using System;
using System.Windows.Input;

namespace TweetSafe.Controls
{
    public class ImageBundle : Image
    {
        public static readonly BindableProperty ImageBundleSourceProperty =
            BindableProperty.Create("ImageBundleSource", typeof(string), typeof(ImageBundle), null,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var stringvalue = (string)newValue;
                });

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ImageBundle), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(ImageBundle), null);

        public static readonly BindableProperty IsPressedProperty =
            BindableProperty.Create("IsPressed", typeof(bool), typeof(ImageBundle), false, BindingMode.TwoWay, null, propertyChanged: ImagePressed);

        public static readonly BindableProperty AnimateProperty =
            BindableProperty.Create("Animate", typeof(bool), typeof(ImageBundle), false);

        private ICommand _imagePressedCommand;

        public ImageBundle()
        {
            Initialize();
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set { SetValue(AnimateProperty, value); }
        }

        public string ImageBundleSource
        {
            set { SetValue(ImageBundleSourceProperty, value); }
            get { return (string)GetValue(ImageBundleSourceProperty); }
        }

        private void Initialize()
        {
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ImagePressedCommand
            });

            TapGestureRecognizer tappedGesture = new TapGestureRecognizer();
            tappedGesture.Tapped += (sender, e) =>
            {
                Pressed?.Invoke(this, new EventArgs());
            };

            GestureRecognizers.Add(tappedGesture);
        }

        public event EventHandler Pressed;

        public ICommand ImagePressedCommand
        {
            get
            {
                return _imagePressedCommand
                       ?? (_imagePressedCommand = new Command(() =>
                       {
                           if (IsPressed)
                           {
                               IsPressed = false;
                           }
                           else
                           {
                               IsPressed = true;
                           }

                           if (Command != null)
                           {
                               Command.Execute(CommandParameter);
                           }
                       }));
            }
        }

        private async static void ImagePressed(BindableObject bindable, object oldValue, object newValue)
        {
            var imageBundle = (ImageBundle)bindable;

            if (Equals(newValue, null) && !Equals(oldValue, null))
                return;

            if (imageBundle.Animate)
            {
                await imageBundle.ScaleTo(0.75, 50, Easing.Linear);
                await imageBundle.ScaleTo(1, 50, Easing.Linear);
            }
        }
    }
}

