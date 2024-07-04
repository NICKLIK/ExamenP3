namespace ExamenP3.Views
{
    public partial class FavoriteCharacterView : ContentPage
    {
        public FavoriteCharacterView()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
