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

            window.MinimumWidth = window.MaximumWidth = 600;
            window.MinimumHeight = window.MaximumHeight = 200;

            return window;
        }
    }
}