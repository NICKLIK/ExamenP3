using ExamenP3.Repositorios;
using ExamenP3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExamenP3.ViewModels
{
    public class CountryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Country> Countries { get; set; }
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }
        public ICommand GetCountriesCommand { get; }
        public ICommand ShowFavoriteCharacterCommand { get; }

        public CountryViewModel()
        {
            Countries = new ObservableCollection<Country>();
            GetCountriesCommand = new Command(async () => await GetCountries());
            ShowFavoriteCharacterCommand = new Command(async () => await ShowFavoriteCharacter());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ShowFavoriteCharacter()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.FavoriteCharacterView());
        }

        private async Task GetCountries()
        {
            var countries = await App.CountryRepo.GetCountriesFromApi();
            foreach (var country in countries)
            {
                country.Codigo = GenerateCode(country.Nombre, country.Region, country.Subregion, country.Status);
                App.CountryRepo.SaveCountry(country);
            }
            LoadCountries();
        }

        private string GenerateCode(string name, string region, string subregion, string status)
        {
            Random random = new Random();
            int number = random.Next(1000, 2000);
            string initials = "JN";
            return $"{initials}{number}_{region}_{subregion}_{status}";
        }

        private void LoadCountries()
        {
            var countries = App.CountryRepo.GetSavedCountries();
            Countries.Clear();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }
    }
}

