using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SafeSpace.Pages;

namespace SafeSpace.ViewModel
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private string _welcomeMessage;
        private Color _color;
        public ICommand NavigateToProfile { get; }
        public ICommand NavigateToChat { get; }
        public ICommand NavigateToHelp { get; }
        public ICommand LogOut{ get; }
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }
        
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public HomePageViewModel()
        {
            var userName = Preferences.Get("UserName", defaultValue:"Nothing");
            
            WelcomeMessage = $"Welcome, {userName}!";
            NavigateToProfile = new Command(async () => await Shell.Current.GoToAsync(nameof(ShowProfilePage)));
            // NavigateToChatCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(LoginPage)));
            NavigateToHelp = new Command(async () => await Shell.Current.GoToAsync(nameof(HelpPage)));
            LogOut = new Command(OnLogOut);
        }
        public async void OnLogOut()
        {
            if (Preferences.ContainsKey("UserName"))
            {
                Preferences.Remove("UserName");
            }
            Preferences.Clear();
            await Task.Delay(500);
            await Shell.Current.GoToAsync("///MainPage");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(backingField, value))
            {
                backingField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
