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

namespace SafeSpace.ViewModel;



public class LoginViewModel : INotifyPropertyChanged
{
    public ICommand NavigateToAboutCommand { get; }
    public ICommand LoginCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;
    public LoginViewModel()
    {
        NavigateToAboutCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(RegisterPage)));
        LoginCommand = new Command(async () => await Login());
    }
    

    private string _userName;
    private string _password;
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
    

    private async Task Login()
    {
        var httpClient = new HttpClient();
        var url = "http://10.0.2.2:5053/api/Auth/login";

        var request = new
        {
            username = UserName,
            password = Password
        };

        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(responseContent);
            var userInfo = jsonDoc.RootElement.GetProperty("user");

            if (response.IsSuccessStatusCode)
            {
                MessageColor = Colors.Green;
                Message = "Login successful!";
                // You could navigate to another page here

                await Task.Delay(500);
                Preferences.Set("UserId", userInfo.GetProperty("id").GetInt32());
                Preferences.Set("UserName", userInfo.GetProperty("name").GetString());
                Preferences.Set("Email", userInfo.GetProperty("email").GetString());
                Preferences.Set("Password", userInfo.GetProperty("password").GetString());

                if (userInfo.TryGetProperty("profile", out var profile))
                {
                    Preferences.Set("ProfileUserId", profile.GetProperty("userId").GetInt32());
                    Preferences.Set("FullName", profile.GetProperty("fullName").GetString());
                    Preferences.Set("Bio", profile.GetProperty("bio").GetString());
                    Preferences.Set("Age", profile.GetProperty("age").GetInt32());
                }


                await Shell.Current.GoToAsync("///HomePage");

            }
            else
            {
                MessageColor = Colors.Red;
                Message = "Login failed: " + responseContent;
            }
        }
        catch (Exception ex)
        {
            MessageColor = Colors.Red;
            Message = $"Error: {ex.Message}";
        }
    }

    private void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
    {
        if (!Equals(backingField, value))
        {
            backingField = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    private async Task NavigateToRegister()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}
