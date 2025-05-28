using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Diagnostics;
using SafeSpace.Pages;


namespace SafeSpace.ViewModel
{
    public class ShowProfilePageViewModel : INotifyPropertyChanged
    {
        private string _fullname;
        private int _age;
        private string _bio;
        private string _concerns;

       
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand EditProfileCommand { get; }
       
        public ICommand SaveChanges {  get; }
        public ShowProfilePageViewModel()
        {
            EditProfileCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(CompleteProfilePage)));

           
            FullName = Preferences.Get("FullName", "Unknown");
            Age = Preferences.Get("Age", 0);
            Bio = Preferences.Get("Bio", "No bio provided");

            Concerns = Preferences.Get("Concerns", "No concerns yet");
        }

        public string FullName
        {
            get => _fullname;
            set => SetProperty(ref _fullname, value);
        }

        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public string Bio
        {
            get => _bio;
            set => SetProperty(ref _bio, value);
        }

        public string Concerns
        {
            get => _concerns;
            set => SetProperty(ref _concerns, value);
        }

        protected void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(backingField, value))
            {
                backingField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
