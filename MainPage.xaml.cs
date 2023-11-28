using DownloadMusic;
using YTPlayer.ViewModels;
using static DownloadMusic.Utility;

namespace YTPlayer
{
	public partial class MainPage : ContentPage
	{
		readonly MainPageViewModel mainPageViewModel;
		public MainPage()
		{
			InitializeComponent();

			mainPageViewModel = new MainPageViewModel();

			mainPageViewModel.WorkCompleted += Template_WorkCompleted;
			mainPageViewModel.SetUpFinished += MainPageViewModel_SetUpFinished;

			BindingContext = mainPageViewModel;
		}

		private void MainPageViewModel_SetUpFinished(object sender, StringArgs e)
		{
			if (e.Title == "Pause")
			{
				PlayOrPauseButton.Command = mainPageViewModel.PauseCommand;
			}
			else if (e.Title == "Resume")
			{
				PlayOrPauseButton.Command = mainPageViewModel.ResumeCommand;
			}
		}

		private async Task Alarm(string title, string message) => await DisplayAlert(title, message, "Ok");
		private async void Template_WorkCompleted(object sender, StringArgs e) => await Alarm("", e.Title);

		private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
		{
			//mainPageViewModel.
		}
	}
}