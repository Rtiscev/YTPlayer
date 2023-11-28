namespace YTPlayer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

			//window.Width =  DeviceDisplay.Current.MainDisplayInfo.Width;
            //window.Height = DeviceDisplay.Current.MainDisplayInfo.Height;

            return window;
        }
    }
}