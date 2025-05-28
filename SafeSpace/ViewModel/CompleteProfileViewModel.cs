using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using SafeSpace.Pages;

namespace SafeSpace.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private int _id;
        private int _userId;
        private string _fullName;
        private int _age;
        private string _bio;

        private string _concern1;
        private string _concern2;
        private string _concern3;

        private readonly Page _page;
        public string Concern1
        {
            get => _concern1;
            set { _concern1 = value; OnPropertyChanged(); }
        }

        public string Concern2
        {
            get => _concern2;
            set { _concern2 = value; OnPropertyChanged(); }
        }

        public string Concern3
        {
            get => _concern3;
            set { _concern3 = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public int UserId
        {
            get => _userId;
            set { _userId = value; OnPropertyChanged(); }
        }

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        public int Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(); }
        }

        public string Bio
        {
            get => _bio;
            set { _bio = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public ProfileViewModel()
        {
            SaveCommand = new Command(async () => await OnSave());
        }

        private async Task OnSave()
        {
            try
            {
                var userId = Preferences.Get("UserId", -1);
                if (userId == -1)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "User ID not found", "OK");
                   
                    return;
                }

                var profile = new
                {
                    userId = userId,
                    fullName = FullName,
                    age = Age,
                    bio = Bio,
                    concerns = new List<string> { Concern1, Concern2, Concern3 }
                };

                var json = JsonSerializer.Serialize(profile);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var httpClient = new HttpClient();
                var response = await httpClient.PutAsync("http://localhost:5053/api/Auth/update", content);

                if (response.IsSuccessStatusCode)
                {
                    
                    Preferences.Set("FullName", FullName);
                    Preferences.Set("Age", Age);
                    Preferences.Set("Bio", Bio);

                    
                    var concernsString = string.Join(", ", new List<string> { Concern1, Concern2, Concern3 });
                    Preferences.Set("Concerns", concernsString);
                    await Application.Current.MainPage.DisplayAlert("Success", "Profile updated", "OK");
                    await Shell.Current.GoToAsync("///HomePage");

                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error", $"Update failed: {errorContent}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
