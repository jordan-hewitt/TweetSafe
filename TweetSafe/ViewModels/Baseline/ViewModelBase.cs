using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TweetSafe.ViewModels
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        //protected readonly INavigationService NavigationService;
        //protected readonly IDialogService DialogService;

        private bool _isBusy;
        private string _title = string.Empty;
        private string _subTitle = string.Empty;
        private string _icon = null;

        public const string TitlePropertyName = "Title";
        public const string SubtitlePropertyName = "Subtitle";
        public const string IconPropertyName = "Icon";

        public ViewModelBase()
        {
            //NavigationService = ViewModelLocator.Resolve<INavigationService>();
            //DialogService = ViewModelLocator.Resolve<IDialogService>();
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, TitlePropertyName); }
        }

        public string Subtitle
        {
            get { return _subTitle; }
            set { SetProperty(ref _subTitle, value, SubtitlePropertyName); }
        }

        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value, IconPropertyName); }
        }

        public virtual Task Initialize(object navigationData)
        {
            return Task.FromResult(false);
        }

        protected void SetProperty<T>(ref T backingStore, T value, string propertyName, Action onChanged = null, Action<T> onChanging = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            if (onChanging != null)
                onChanging(value);

            OnPropertyChanging(propertyName);

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }
    }
}
