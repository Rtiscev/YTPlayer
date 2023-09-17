using DownloadMusic;
using YTPlayer.ViewModels;

namespace YTPlayer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            MainPageViewModel mainPageViewModel = new MainPageViewModel();
            mainPageViewModel.WorkCompleted += Template_WorkCompleted;

            //PlayOrPauseButton.comm

            BindingContext = mainPageViewModel;

        }
        private async Task Alarm(string title, string message) => await DisplayAlert(title, message, "Ok");
        private async void Template_WorkCompleted(object sender, Utility.StringArgs e) => await Alarm("", e.Title);

    }
}