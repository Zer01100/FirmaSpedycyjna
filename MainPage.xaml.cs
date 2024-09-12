namespace FirmaSpedycyjna
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            //var pracownicy = _databaseService.GetPracownicy();
            
        }

        private void OnPointerEntered(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.BackgroundColor = Color.FromArgb("#2b2d2d");
            }
        }

        private void OnPointerExited(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.BackgroundColor = Color.FromArgb("#383b3b");
            }
        }
    }

}
