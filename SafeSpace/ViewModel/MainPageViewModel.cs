using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Text.Json;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SafeSpace.Pages;

namespace SafeSpace.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }
        public ICommand NavigateToGuestCommand { get; }

        private string _message;
        private Color _messageColor;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            NavigateToRegisterCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(RegisterPage)));
            NavigateToLoginCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(LoginPage)));
            NavigateToGuestCommand = new Command(async () => await GuestLogin());
        }
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public Color MessageColor
        {
            get => _messageColor;
            set => SetProperty(ref _messageColor, value);
        }
        private async Task GuestLogin()
        {
            var httpClient = new HttpClient();
            var url = "http://localhost:5053/api/Auth/guest-login";

            try
            {
                var response = await httpClient.PostAsync(url, null);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<GuestResponse>(responseContent);

                    
                    Preferences.Set("GuestUserName", result.guestName);
                    Preferences.Set("GuestUserId", result.userId.ToString());

                    MessageColor = Colors.Green;
                    Message = $"Welcome, {result.guestName}!";

                    await Task.Delay(1000);
                    await Shell.Current.GoToAsync("///HomePage");
                }
                else
                {
                    MessageColor = Colors.Red;
                    Message = "Guest login failed";
                }
            }
            catch (Exception ex)
            {
                MessageColor = Colors.Red;
                Message = $"Error: {ex.Message}";
            }
        }

        private class GuestResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
            public string guestName { get; set; }
            public int userId { get; set; }
        }
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
