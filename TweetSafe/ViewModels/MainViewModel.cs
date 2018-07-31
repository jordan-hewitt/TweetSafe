
using TweetSafe.Helpers;

namespace TweetSafe.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private double _imageWidth;

        public MainViewModel()
        {
            _imageWidth = 200;
        }

        public double ImageWidth
        {
            get { return _imageWidth; }
            set
            {
                _imageWidth = value;
                RaisePropertyChanged(() => ImageWidth);
            }
        }
    }
}
