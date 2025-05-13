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
    public class RegisterPageViewModel : INotifyPropertyChanged 
    {
        public ICommand RegisterCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(async () => await Register());
        }

        private string _userName;
        private string _password;
        private string _email;
        private string _message;
        private Color _messageColor;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);

        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
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

        private void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(backingField, value))
            {
                backingField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async Task Register()
        {
            var httpClient = new HttpClient();
            var url = "http://localhost:5053/api/Auth/register"; // Change if needed

            var request = new
            {
                username = UserName,
                password = Password,
                email = Email
            };

            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageColor = Colors.Green;
                    Message = "Register successful!";
                    await Shell.Current.GoToAsync("///MainPage");
                }
                else
                {
                    MessageColor = Colors.Red;
                    Message = "Register failed: " + responseContent;
                }
            }
            catch (Exception ex)
            {
                MessageColor = Colors.Red;
                Message = $"Error: {ex.Message}";
            }
        }
    }

}
