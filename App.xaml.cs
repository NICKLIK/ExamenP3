using CountryApp.Repositorios;

namespace ExamenP3
{
    public partial class App : Application{
        public static CountryRepository CountryRepo { get; private set; }
    public App()    
    {
        {
            InitializeComponent();

                CountryRepo = new CountryRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "countries.db3"));

                MainPage = new NavigationPage(new Views.CountryView());
            }
    }
   }
}
    