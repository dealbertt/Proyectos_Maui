using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace SafeSpace.ViewModel
{
    class LoginPageViewModel : BindableObject
    {
        private string _userName;
        private string _password;
        private string _message;
        private Color _messagecolor;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public Color MessageColor
        {
            get => _messagecolor;
            set
            {
                _messagecolor = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public LoginPageViewModel() {
            LoginCommand = new Command(OnLogin);
        }
        private async void OnLogin()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)) {
                await Shell.Current.DisplayAlert("Error", "Please enter both", "Ok");
                return;
            }

            if (UserName == "dealbertt" && Password == "password")
            {
                Message = "Login Successful!";
                MessageColor = Colors.Green;

                // Wait a moment before navigating (optional)
                

                // Navigate to MainPage after success
            }
            else
            {
                Message = "Invalid username or password.";
                MessageColor = Colors.Red;
            }
        }

    }
}