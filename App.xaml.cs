namespace FirmaSpedycyjna
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new KlienciStrona());
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Width = 1920;
            window.Height = 1080;

            window.MinimumWidth = 1920;
            window.MinimumHeight = 1080;

            return window;
        }
    }
}
